using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using System;
using System.Linq;
using Xunit;

namespace SafetyFund.Data.Tests
{
    public class CampaignRepoTests : AbstractBaseTests
    {
        private readonly CampaignRepo repo;


        public CampaignRepoTests()
        {
            this.repo = new CampaignRepo(GetDbOptions());
        }


        public Campaign CreateCampaign()
        {
            var maxEndDateTime = DateTime.Now;

            var campaigns = repo.Get();

            maxEndDateTime = campaigns.Aggregate(maxEndDateTime, (current, item) => (current <= item.EndDateTime) ? item.EndDateTime : current);

            var campaign = new Campaign
            {
                Title = "Campaign",
                StartDateTime = DateTime.Now.AddDays(-10),
                EndDateTime = maxEndDateTime.AddDays(5)
            };

            repo.Add(campaign);

            return campaign;
        }


        [Fact]
        public void When_campaign_is_added_then_it_can_be_loaded_from_DB()
        {
            var campaign = CreateCampaign();

            var campaignFromDb = repo.Get(campaign.Id);

            Assert.NotNull(campaignFromDb);
            Assert.Equal(campaignFromDb.Id, campaign.Id);
        }


        [Fact]
        public void When_campaign_is_deleted_from_DB_it_cannot_be_found_in_DB()
        {
            var campaign = CreateCampaign();

            repo.Delete(campaign);

            var campaignFromDb = repo.Get(campaign.Id);

            Assert.Null(campaignFromDb);
        }


        [Fact]
        public void When_campaign_added_to_DB_then_it_isContained_in_all_campaignList_FromDB()
        {
            var campaign = CreateCampaign();

            var campaigns = repo.Get();

            Assert.Contains(campaigns, item => item.Id == campaign.Id);
        }


        [Fact]
        public void When_campaign_edited_then_modifications_applied_To_existing_campaign_in_DB()
        {
            var campaign = CreateCampaign();

            var campaignFromDb = repo.Get(campaign.Id);

            var campaignDataToChange = new Campaign
            {
                Id = campaignFromDb.Id,
                Title = campaignFromDb.Title + " Changed",
                StartDateTime = campaignFromDb.StartDateTime.AddYears(-2),
                EndDateTime = campaignFromDb.EndDateTime.AddYears(-1)
            };

            repo.Update(campaignDataToChange);

            var campaignFromDbAfterChanges = repo.Get(campaignFromDb.Id);

            Assert.Equal(campaignFromDbAfterChanges.Id, campaignFromDb.Id);
            Assert.NotEqual(campaignFromDbAfterChanges.Title, campaignFromDb.Title);
            Assert.NotEqual(campaignFromDbAfterChanges.StartDateTime, campaignFromDb.StartDateTime);
            Assert.NotEqual(campaignFromDbAfterChanges.EndDateTime, campaignFromDb.EndDateTime);
        }


        [Fact]
        public void When_getting_newest_campaign_it_returns_newest_campaign()
        {
            var lastCampaign = CreateCampaign();
            var campaignExpectedToBeNewest = repo.GetNewestCampaign();

            Assert.Equal(campaignExpectedToBeNewest.Id, lastCampaign.Id);
        }


        [Fact]
        public void When_campaign_contains_atLeast_1_project_Then_correct_campaign_repo_is_generated()
        {
            var projectRepoTests = new ProjectRepoTests();

            var campaign = CreateCampaign();

            projectRepoTests.CreateProject(campaign.Id);
            projectRepoTests.CreateProject(campaign.Id);

            var report = repo.GetCampaignsAndProjectCounts();

            var campaignFromReport = report.First(r => r.Campaign.Id == campaign.Id);

            Assert.Equal(2, campaignFromReport.ProjectCount);
        }


        [Fact]
        public void When_campaign_contains_no_projects_Then_correct_campaing_repo_is_generated()
        {
            var campaign = CreateCampaign();
            var report = repo.GetCampaignsAndProjectCounts();

            var campaignFromReport = report.First(r => r.Campaign.Id == campaign.Id);

            Assert.Equal(0, campaignFromReport.ProjectCount);
        }
    }
}
