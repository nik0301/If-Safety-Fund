using System;
using System.Collections.Generic;
using Moq;
using SafetyFund.Data.Models;
using SafetyFund.Data.Reports;
using SafetyFund.Data.Repositories;
using SafetyFund.Data.Tests;
using Xunit;

namespace SafetyFund.Business.Tests
{
    public class CampaignListTests : AbstractBaseTests
    {
        private readonly CampaignList campaignList;
        private readonly Mock<CampaignRepo> repoMock;


        public CampaignListTests()
        {
            repoMock = new Mock<CampaignRepo>(null);
            campaignList = new CampaignList(repoMock.Object);
        }


        [Fact]
        public void When_Get_Then_Result_Is_ListOf_Campaigns()
        {
            // Setup
            var campaigns = new List<Campaign>
            {
                new Campaign
                {
                    Id = 1
                },

                new Campaign
                {
                    Id = 2
                }
            };

            repoMock
                .Setup(r => r.Get())
                .Returns(campaigns);

            // Act
            var campaignsFromDb = campaignList.Get();

            // Assert
            Assert.Equal(campaigns, campaignsFromDb);
        }


        [Fact]
        public void When_create_campaign_and_delete_campaign_By_id()
        {
            var campaign = new Campaign()
            {
                Id = 123123
            };

            repoMock
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns(campaign);
            repoMock
                .Setup(r => r.Delete(It.IsAny<Campaign>()));

            campaignList.DeleteById(123123);

            repoMock.Verify(r => r.Delete(It.IsAny<Campaign>()), Times.Once);
        }


        [Fact]
        public void When_add_new_campaign_then_it_exists_in_db()
        {
            var campaign = new Campaign()
            {
                Id = 12345
            };

            repoMock
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns(campaign);
            repoMock
                .Setup(r => r.Add(It.IsAny<Campaign>()));

            campaignList.Add(campaign);

            repoMock.Verify(r => r.Add(It.IsAny<Campaign>()), Times.Once);
        }


        [Fact]
        public void When_updating_campaign_Update_does_campaign_Update()
        {
            var testCampaign = new Campaign();

            repoMock
                .Setup(r => r.Update(testCampaign));

            campaignList.Update(testCampaign);

            //Assert
            repoMock.Verify(r => r.Update(testCampaign), Times.Once);
        }


        [Fact]
        public void When_Getting_LastCampaign_GetLastCampaign_returns_LastCampaign()
        {
            var latestCampaign = new Campaign
            {
                Id = 1
            };

            repoMock
                .Setup(r => r.GetNewestCampaign())
                .Returns(latestCampaign);

            //Act
            var resultCampaign = campaignList.GetLastCampaign();

            //Assert
            Assert.Equal(latestCampaign.Id, resultCampaign.Id);
            repoMock.Verify(r => r.GetNewestCampaign(), Times.Once);
        }


        [Fact]
        public void When_Getting_CampaignsWithProjectCounts_Returns_CampaignReport()
        {
            repoMock
                .Setup(r => r.GetCampaignsAndProjectCounts());

            campaignList.GetCampaignsWithProjectCounts();

            repoMock.Verify(r => r.GetCampaignsAndProjectCounts(), Times.Once);
        }


        [Fact]
        public void When_Getting_Campaign_By_Id_returns_campaignWithGivenId()
        {
            var campaign = new Campaign()
            {
                Id = 1,
                EndDateTime = DateTime.Now,
                StartDateTime = DateTime.Now
            };

            repoMock
                .Setup(r => r.Get(campaign.Id))
                .Returns(campaign);

            Assert.Equal(campaign, campaignList.Get(campaign.Id));
        }
    }
}
