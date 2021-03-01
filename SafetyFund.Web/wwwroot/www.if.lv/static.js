$.ajax({
}).done(function () {
    if (window.location.href.indexOf("?") >= 0) {
        $.ajax({
            method: "GET",
            url: "/api/VoteApi/VoteDraugiem",
            data: { locationHref: window.location.href }
        }).done(function (voteInfo) {
            window.history.pushState({}, document.title, "/publicsitetest");
            indicateVoteSuccess(voteInfo.value.isVoteSuccesful, voteInfo.value.projectId);

            if (voteInfo.value.isVoteSuccesful) {
                draugiemShare(voteInfo.value.projectId);
            }
        });
    }
});


window.fbAsyncInit = function () {
    FB.init({
        appId: '142672243226334',
        autoLogAppEvents: true,
        xfbml: true,
        version: 'v2.12'
    });
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s);
    js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));


var projectCount;
var closedRowHeights = [];

$.ajax({
    method: "GET",
    url: "/api/ProjectApi/GetLastCampaignProjects",
    //url: "https://safetyfund-test.azurewebsites.net/api/ProjectApi/GetLastCampaignProjects",
    dataType: "json",
    success: function (projectsAndCampaignUrlModel) {
        showProjects(projectsAndCampaignUrlModel);
    },
    error: function (xhr, status, error) {
        if (error === 'Forbidden') {
            $("#main-container")
                .append('<p class="item-text-center">Atvainojiet, bet balsošana no jūsu lokācijas nav atļauta.</p>');
        }
    }
});


function getProjectHTML(project, projectBoxId, isCampaignActive) {
    return '<div class="project-wrapper" id="proj-wrapper-' + projectBoxId + '">' //Project box wrapper
        + '     <div class="project-box" id="project-box-' + projectBoxId + '">'
        + '         <div class="item-container votes-text" id="votes-id-' + project.project.id + '">Balsis: ' + project.votes + "</div>"

        + '         <div class="image-wrapper">'
        + '             <img id="img-id-' + project.project.id + '" src="data:image/jpg;base64,' + project.project.image + '">' //image
        + "         </div>"

        + '         <div class="item-container text-container">'
        + '             <div class="item-text-center project-title" id="title-id-' + project.project.id + '">' + project.project.title + "</div>" //title

        + '             <div class="project-intro">' + project.project.intro + "</div>"
        + '             <div class="project-desc" id="project-desc-id-' + projectBoxId + '">' + project.project.description + "</div>"
        + '             <div class="readmore" id="readmore-id-' + projectBoxId + '"onclick="showDescription(' + projectBoxId + ')">Uzzināt vairāk</div>'
        + "         </div>"

        + getSocialVotingArea()

        + "     </div>"
        + "</div>"; //end of project-box and wrapper

    function getSocialVotingArea() {
        return (!isCampaignActive) ? "" //if campaign is not active, social media area is not needed
            : '        <div class="item-text-center vote-now-text">Balso:</div>'
            + '        <div class="item-container vote-area">'
            + '                 <div class="soc-btn fb-vote" ' + getBtnHtmlAttributes("Facebook") + "></div>"
            + '                 <div class="soc-btn draugiem-vote" ' + getBtnHtmlAttributes("Draugiem") + "></div>"
            + "         </div>";

        function getBtnHtmlAttributes(socMediaName) {
            return ' onclick="vote' + socMediaName + "(" + project.project.id + ')"';
        }
    }
}


function voteFacebook(projectId) {
    FB.login(function (response) {
        if (response.status === "connected") {
            var voteData = { projectId: projectId, userId: response.authResponse.userID, socialName: "Facebook" };
            sendVoteDataToApi(voteData);
        }
    }, { scope: 'public_profile' }
    );
}


function facebookShare(projectId) {
    var projectTitle = $("#title-id-" + projectId).html();

    FB.ui({
        method: 'share_open_graph',
        action_type: 'og.shares',
        action_properties: JSON.stringify({
            object: {
                'og:url': window.location.href,
                'og:title': 'Es nobalsoju par projektu "' + projectTitle + "! Nobalso arī Tu!'",
                'og:description': 'If.lv drošības fonds',
                'og:image': "https://safetyfund-test.azurewebsites.net/api/VoteApi/LoadProjectImage/" + projectId
            }
        })
    }, function (response) {
    });
}


function voteDraugiem(projectId) {
    $.ajax({
        method: "GET",
        url: "/api/VoteApi/SetProjectAndGetDraugiemAuthLink",
        dataType: "json",
        data: { projectId: projectId }
    }).done(function (url) {
        $(location).attr('href', url);
    });
}


function draugiemShare(projectId) {
    var title = $("#title-id-" + projectId).html();
    var desc = 'Es nobalsoju par projektu"' + title + '"! Nobalso arī tu!';

    window.open(
        'http://www.draugiem.lv/say/ext/add.php?title=' + encodeURIComponent(desc)
        + '&link=' + encodeURIComponent("https://safetyfund-test.azurewebsites.net/publicsitetest#" + "votes-id-" + projectId),
        '',
        '&location=0,status=0,scrollbars=0,resizable=0,width=530,height=400'
    );
}


function sendVoteDataToApi(voteData) {
    $.ajax({
        method: "GET",
        url: "/api/VoteApi/GiveVote",
        dataType: "json",
        data: { projectId: voteData["projectId"], userId: voteData["userId"], socialName: voteData["socialName"] }
    }).done(function (voteResponse) {
        indicateVoteSuccess(voteResponse.isVoteSuccesful, voteResponse.projectId);
        if (voteResponse.isVoteSuccesful) {
            facebookShare(voteResponse.projectId);
        }
    });
}


function indicateVoteSuccess(isVotingSuccesful, projectId) {
    $("#votes-id-" + projectId).get(0).scrollIntoView();
    window.scrollBy(0, -20);

    if (isVotingSuccesful) {
        $("#votes-id-" + projectId).html("balss ieskaitīta");
    } else {
        $("#votes-id-" + projectId).html("jau nobalsots");
    }

    reloadVoteCount(projectId);
}


function reloadVoteCount(projectId) {
    $.ajax({
        method: "Get",
        url: "/api/VoteApi/ReloadProjectVotes",
        dataType: "json",
        data: { projectId: projectId }
    }).done(function (votesCount) {
        setTimeout(function () { $("#votes-id-" + projectId).html("Balsis: " + votesCount); }, 1500);
    });
}


function showProjects(projectsAndCampaignUrlModel) {
    projectCount = projectsAndCampaignUrlModel.projects.length;

    if (projectCount !== 0) {
        if (!projectsAndCampaignUrlModel.isCampaignActive) {
            $("#main-container").append("<p>Šobdrīd nav aktīvās kampaņas. </br>Pagājušās kampaņas rezultāti:</p>");
        }

        var projectBoxId = 0;

        projectsAndCampaignUrlModel.projects.forEach(function (project) {
            $("#main-container").append(getProjectHTML(project, projectBoxId, projectsAndCampaignUrlModel.isCampaignActive));
            projectBoxId++;
        });

        resizeProjects();

        $(window).resize(function () {
            resizeProjects();
        });
    } else {
        $("#main-container").append("<p>Kampaņai nav pievienotu projektu</p>");
    }
}


function showDescription(projectBoxId) {
    var projectDescHtmlBlock = $("#project-desc-id-" + projectBoxId);
    var readMoreHtmlBlock = $("#readmore-id-" + projectBoxId);
    var textContainerHtmlBlock = $("#project-box-" + projectBoxId + " .text-container");

    if (readMoreHtmlBlock.html() === "Uzzināt vairāk") {
        projectDescHtmlBlock.css("display", "block");
        readMoreHtmlBlock.html("Aizvērt");
        textContainerHtmlBlock.css("height", "auto");
        resizeProjectWhenOpenedDesc(projectBoxId);
    } else {
        projectDescHtmlBlock.css("display", "none");
        readMoreHtmlBlock.html("Uzzināt vairāk");
        textContainerHtmlBlock.css("height", "auto");
        resizeProjectWhenClosedDesc(projectBoxId);
    }
}


function resizeProjects() {
    if (projectCount === undefined) {
        return;
    }

    var projectsCountInTableRow = getProjectsCountInTableRow();
    setClosedRowContainerHeights();

    for (let id = 0; id < projectCount; id += projectsCountInTableRow) {
        resizeRowTextContainers(id);
        applyElementHeights(getRowElements("wrapper", id, id + projectsCountInTableRow), true);
    }
}


function resizeProjectWhenOpenedDesc(projectBoxId) {
    var projectsCountInTableRow = getProjectsCountInTableRow();
    var firstProjectInRowId = projectBoxId - projectBoxId % projectsCountInTableRow;

    $(getOneElement("text-container", projectBoxId)).height("auto");
    $(getOneElement("wrapper", projectBoxId)).height("auto");

    applyElementHeights(getRowElements("wrapper", firstProjectInRowId, firstProjectInRowId + projectsCountInTableRow), true);
}


function resizeProjectWhenClosedDesc(projectBoxId) {
    var projectsCountInTableRow = getProjectsCountInTableRow();
    var firstProjectInRowId = projectBoxId - projectBoxId % projectsCountInTableRow;

    var rowNumber = Math.floor(projectBoxId / projectsCountInTableRow);

    var closedBox = $("#project-box-" + projectBoxId + " .text-container").toArray();
    $(closedBox).height(closedRowHeights[rowNumber]);

    applyElementHeights(getRowElements("wrapper", firstProjectInRowId, firstProjectInRowId + projectsCountInTableRow), true);
}


function getRowElements(elementName, startId, lastId, excludedId) {
    var elements = [];

    for (let id = startId; id < lastId; id++) {
        if (id !== excludedId) {
            elements.push(getOneElement(elementName, id));
        }
    }

    return elements;
}


function getOneElement(elementName, id) {
    if (elementName === "text-container") {
        return $("#project-box-" + id + " .text-container").toArray();
    } else if (elementName === "wrapper") {
        return $("#proj-wrapper-" + id).toArray();
    }

    return undefined;
}


function getRowElementMaxHeight(elements, isSizeRefreshNeeded) {
    var maxHeight = 0;

    elements.forEach(function (x) {
        if (isSizeRefreshNeeded) {
            $(x).height("auto");
        }
        var tempMax = $(x).height();
        maxHeight = (tempMax > maxHeight) ? tempMax : maxHeight;
    });

    return maxHeight;
}


function resizeRowTextContainers(firstProjectInRowId, clickedProjectBoxId) {
    var projectsCountInTableRow = getProjectsCountInTableRow();
    var textContainers = getRowElements("text-container", firstProjectInRowId, firstProjectInRowId + projectsCountInTableRow, clickedProjectBoxId);

    applyElementHeights(textContainers, false);
}


function getProjectsCountInTableRow() {
    var bootstrapSm = 661;
    var bootstrapMd = 981;

    var windowSize = $(window).width();

    if (windowSize >= bootstrapMd) {
        return 3;
    } else if (windowSize < bootstrapSm) {
        return 1;
    } else {
        return 2;
    }
}


function setClosedRowContainerHeights() {
    closedRowHeights = [];
    closeAllDescriptions();

    var projectsCountInTableRow = getProjectsCountInTableRow();

    for (let id = 0; id < projectCount; id += projectsCountInTableRow) {
        var textContainers = getRowElements("text-container", id, id + projectsCountInTableRow);
        closedRowHeights.push(getRowElementMaxHeight(textContainers, false));
    }

    function closeAllDescriptions() {
        for (let id = 0; id < projectCount; id++) {
            var projectDescHtmlBlock = $("#project-desc-id-" + id);
            var readMoreHtmlBlock = $("#readmore-id-" + id);
            var textContainerHtmlBlock = $("#project-box-" + id + " .text-container");

            projectDescHtmlBlock.css("display", "none");
            readMoreHtmlBlock.html("Uzzināt vairāk");
            textContainerHtmlBlock.css("height", "auto");
        }
    }
}


function applyElementHeights(elements, isSizeRefreshNeeded) {
    var maxHeight = getRowElementMaxHeight(elements, isSizeRefreshNeeded);

    elements.forEach(function (x) {
        return $(x).height(maxHeight);
    });
}
