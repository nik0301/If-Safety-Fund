using System;
using System.Collections.Generic;
using SafetyFund.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SafetyFund.Business;
using SafetyFund.Data.Models;
using SafetyFund.Data.Reports;
using Xunit;
using SafetyFund.Data.Tests;

namespace SafetyFund.Web.Tests
{
    public class HomeControllerTests : AbstractBaseTests
    {
        private readonly HomeController controller;
        private readonly Mock<CampaignList> campaignListMock;
        private readonly Mock<ProjectList> projectListMock;


        public HomeControllerTests()
        {
            campaignListMock = new Mock<CampaignList>(null);
            projectListMock = new Mock<ProjectList>(null);

            controller = new HomeController(campaignListMock.Object, projectListMock.Object);
        }


        [Fact]
        public void When_Calling_Index_Then_Returns_Index()
        {
            // Setup
            campaignListMock
                .Setup(r => r.GetCampaignsWithProjectCounts());

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void When_Calling_Details_Then_Return_Details()
        {
            // Setup
            var campaign = new Campaign
            {
                Id = 1,
                Title = "rtrtr",
                StartDateTime = DateTime.Now.AddDays(-1),
                EndDateTime = DateTime.Now.AddDays(1)
            };

            projectListMock
                .Setup(r => r.GetProjectsWithVotesCount(campaign.Id, DateTime.Now))
                .Returns((List<ProjectReport>)null);

            campaignListMock
                .Setup(r => r.Get(campaign.Id))
                .Returns(campaign);

            // Act
            var result = controller.Details(campaign.Id) as ViewResult;

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void When_CampaignId_Has_value_And_Calling_CreateEdit_Then_Return_CreateEdit()
        {
            // Setup
            var campaign = new Campaign
            {
                Id = 1,
                Title = "rtrtr",
                StartDateTime = DateTime.Now.AddDays(-1),
                EndDateTime = DateTime.Now.AddDays(1)
            };

            campaignListMock
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns(campaign);

            // Act
            var result = controller.CreateEdit(campaign.Id) as ViewResult;

            //Assert
            Assert.NotNull(result);
            campaignListMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public void When_CampaignId_Has_noValue_And_Calling_CreateEdit_Then_Return_CreateEdit()
        {
            // Act
            var result = controller.CreateEdit((int?)null);

            campaignListMock
                .Setup(r => r.Get(It.IsAny<int>()));

            // Assert
            Assert.NotNull(result);
        }
    }
}