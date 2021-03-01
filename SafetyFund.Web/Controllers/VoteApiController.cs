using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;
using Newtonsoft.Json;
using SafetyFund.Business;

namespace SafetyFund.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/VoteApi")]
    public class VoteApiController : Controller
    {
        private readonly VoteList voteList;
        private readonly ProjectList projectList;
        private readonly LocationService locationService;

        private const string PROJECT_ID_NAME = "_ProjectId";

        private const string API_DRAUGIEM_AUTHORIZE_JSON_LINK = "http://api.draugiem.lv/json/?app=";
        private const string APP_API_KEY = "202958109b724c33039037f75596d238";
        private const string APP_ID = "15020783";
        private const string REDIRECT_URL = "http://localhost:53237/publicsitetest";


        public VoteApiController(VoteList voteList, ProjectList projectList, LocationService locationService)
        {
            this.voteList = voteList;
            this.locationService = locationService;
            this.projectList = projectList;
        }

        private bool IsCountryAllowed() => locationService.IsCountryAllowed(Request.HttpContext.Connection.RemoteIpAddress);


        [Route("GiveVote")]
        [HttpGet]
        public IActionResult GiveVote(int projectId, string userId, string socialName)
        {
            if (!IsCountryAllowed())
            {
                return Forbid();
            }

            try
            {
                voteList.AddVote(projectId, userId, socialName);
                return Ok(new VoteInfo(true, projectId));
            }
            catch (UserAlreadyVotedException)
            {
                return Ok(new VoteInfo(false, projectId));
            }
        }


        [Route("VoteDraugiem")]
        [HttpGet]
        public IActionResult VoteDraugiem(string locationHref)
        {
            if (!IsCountryAllowed())
            {
                return Forbid();
            }

            var projectId = HttpContext.Session.GetInt32(PROJECT_ID_NAME) ?? 0;

            try
            {
                var userId = GetDraugiemUserId(GetDraugiemUserDataRequestLink(locationHref));
                return Ok(GiveVote(projectId, userId, "Draugiem"));
            }
            catch (InvalidAuthStatusException)
            {
                return Ok(new VoteInfo(false, projectId));
            }
        }


        private string GetDraugiemUserId(string requestLink)
        {
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(requestLink))
                {
                    using (var content = response.Result.Content)
                    {
                        return JsonConvert.DeserializeObject<dynamic>(content.AsString()).uid.Value.ToString();
                    }
                }
            }
        }


        private string GetDraugiemUserDataRequestLink(string locationHref)
        {
            var drAuthCode = GetDraugiemAuthCode(locationHref);
            var actionUrl = $"{API_DRAUGIEM_AUTHORIZE_JSON_LINK}{APP_API_KEY}&code={drAuthCode}&action=authorize";

            return actionUrl;
        }


        private static string GetDraugiemAuthCode(string locationHref)
        {
            var drAuthStatus = locationHref.Split('?')[1].Split('&')[0].Split('=')[1];

            if (drAuthStatus.ToLower() == "ok")
            {
                return locationHref.Split('?')[1].Split('&')[1].Split('=')[1];
            }

            throw new InvalidAuthStatusException();
        }


        [Route("SetProjectAndGetDraugiemAuthLink")]
        [HttpGet]
        public IActionResult SetProjectIdForVoteAndReturnAuthUrl(int projectId)
        {
            if (!IsCountryAllowed())
            {
                return Forbid();
            }

            HttpContext.Session.SetInt32(PROJECT_ID_NAME, projectId);

            var hash = GetMd5Hash(APP_API_KEY + REDIRECT_URL);

            return Ok($"http://api.draugiem.lv/authorize/?app={APP_ID}&hash={hash}&redirect={REDIRECT_URL}");
        }


        public static string GetMd5Hash(string input)
        {
            var sBuilder = new StringBuilder();
            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                foreach (var item in data)
                {
                    sBuilder.Append(item.ToString("x2"));
                }
            }

            return sBuilder.ToString();
        }


        [Route("ReloadProjectVotes")]
        [HttpGet]
        public IActionResult ReloadProjectVotes(int projectId)
        {
            if (!IsCountryAllowed())
            {
                return Forbid();
            }

            return Ok(voteList.GetVotesCountByProject(projectId));
        }


        [Route("LoadProjectImage")]
        [HttpGet]
        public IActionResult LoadProjectImage(int projectId)
        {
            var img = projectList.GetById(projectId).Image;

            return (img != null)
                ? File(img, "image/jpg")
                : null;
        }
    }
}