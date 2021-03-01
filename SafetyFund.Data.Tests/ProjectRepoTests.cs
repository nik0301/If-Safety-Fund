
using System;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using Xunit;

namespace SafetyFund.Data.Tests
{
    public class ProjectRepoTests : AbstractBaseTests
    {
        private readonly ProjectRepo repo;


        public ProjectRepoTests()
        {
            repo = new ProjectRepo(GetDbOptions());
        }


        public Project CreateProject(int campaignId = -1)
        {
            var project = new Project
            {
                Title = "Project for testing",
                Intro = "bla bla bla",
                Description = "This is a projects, which is used for testing project repository",
                Image = new byte[] { 255, 255, 255, 255 },
                CampaignId = (campaignId == -1) ? new CampaignRepoTests().CreateCampaign().Id : campaignId,
                OrderNumber = 2
            };

            repo.Add(project);

            return project;
        }


        [Fact]
        public void When_project_is_added_then_it_can_be_loaded_from_DB()
        {
            var project = CreateProject();

            var projectFromDb = repo.Get(project.Id);

            Assert.NotNull(projectFromDb);
            Assert.Equal(projectFromDb.Id, project.Id);
        }


        [Fact]
        public void When_project_is_deleted_from_DB_it_cannot_be_found_in_DB()
        {
            var project = CreateProject();
            repo.Delete(project);
            var projectFromDb = repo.Get(project.Id);

            Assert.Null(projectFromDb);
        }


        [Fact]
        public void When_project_added_to_DB_then_it_isContained_in_all_projectList_FromDB()
        {
            var project = CreateProject();
            var projects = repo.Get();

            Assert.Contains(projects, item => item.Id == project.Id);
        }


        [Fact]
        public void When_project_edited_then_modifications_applied_To_existing_project_in_DB()
        {
            var project = CreateProject();
            var projectFromDb = repo.Get(project.Id);

            var projectDataToChange = new Project()
            {
                Id = projectFromDb.Id,
                Title = projectFromDb.Title + " Changed",
                Image = projectFromDb.Image,
                Intro = projectFromDb.Intro + "changed bla",
                Description = projectFromDb.Description + " Modified",
                CampaignId = projectFromDb.CampaignId
            };

            repo.Update(projectDataToChange);

            var projectFromDbAfterChanges = repo.Get(projectFromDb.Id);

            Assert.Equal(projectFromDbAfterChanges.Id, projectFromDb.Id);
            Assert.NotEqual(projectFromDbAfterChanges.Title, projectFromDb.Title);
        }


        [Fact]
        public void When_getting_project_by_campaign_then_result_contains_only_projects_with_given_campaign_ID()
        {
            var campaign = new CampaignRepoTests().CreateCampaign();
            CreateProject(campaign.Id);

            var projectsByCampaign = repo.GetByCampaign(campaign.Id);

            Assert.Contains(projectsByCampaign, item => item.CampaignId == campaign.Id);
            Assert.DoesNotContain(projectsByCampaign, item => item.CampaignId != campaign.Id);
        }


        [Fact]
        public void When_create_campaign_and_and_projects_in_campaign_and_add_some_votes_then_return_back_count_of_projects_and_count_of_project_votes()
        {
            var campaign = new CampaignRepoTests().CreateCampaign();
            var project1 = CreateProject(campaign.Id);
            var project2 = CreateProject(campaign.Id);
            var project3 = CreateProject(campaign.Id);

            var voteRepo = new VoteRepo(GetDbOptions());

            voteRepo.Add(new Vote()
            {
                UserId = "ppppffpfp",
                VotingDateTime = DateTime.Today.AddDays(-5),
                SocialName = "facebook",
                ProjectId = project1.Id
            });

            voteRepo.Add(new Vote()
            {
                UserId = "ppppfp",
                VotingDateTime = DateTime.Today.AddDays(-15),
                SocialName = "facebook",
                ProjectId = project2.Id
            });

            voteRepo.Add(new Vote()
            {
                UserId = "pppd",
                VotingDateTime = DateTime.Today.AddDays(-3),
                SocialName = "facebook",
                ProjectId = project2.Id
            });

            var report = repo.GetProjectsReport(campaign.Id);

            Assert.Equal(3, report.Count);
            Assert.Equal(project1.Id, report[0].Project.Id);
            Assert.Equal(project2.Id, report[1].Project.Id);
            Assert.Equal(project3.Id, report[2].Project.Id);

            Assert.Equal(1, report[0].Votes);
            Assert.Equal(2, report[1].Votes);
            Assert.Equal(0, report[2].Votes);
        }
    }
}
