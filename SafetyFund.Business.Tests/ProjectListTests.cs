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
    public class ProjectListTests : AbstractBaseTests
    {
        private readonly ProjectList projectList;
        private readonly Mock<ProjectRepo> repoMock;

        public ProjectListTests()
        {
            repoMock = new Mock<ProjectRepo>(null);
            projectList = new ProjectList(repoMock.Object);
        }


        [Fact]
        public void When_deleting_project_from_db_deletingMethod_is_returning_its_campaignId()
        {
            // Setup
            var project = new Project
            {
                Id = 2,
                CampaignId = 1
            };

            repoMock
                .Setup(r => r.Get(project.Id))
                .Returns(project);
            repoMock
                .Setup(r => r.Delete(It.IsAny<Project>()));

            // Act
            var expectedCampaignId = projectList.DeleteProjectAndGetCampaignId(project.Id);

            // Assert
            Assert.Equal(1, expectedCampaignId);
            repoMock.Verify(r => r.Delete(It.IsAny<Project>()), Times.Once);
            repoMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public void When_calling_UpdateProject_passing_newProject_it_adds_new_project_in_DB()
        {
            // Setup
            repoMock
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns((Project)null);
            repoMock
                .Setup(r => r.Update(It.IsAny<Project>()));
            repoMock
                .Setup(r => r.Add(It.IsAny<Project>()));

            // Act
            projectList.UpdateProject(new Project());

            // Assert
            repoMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
            repoMock.Verify(r => r.Update(It.IsAny<Project>()), Times.Never);
            repoMock.Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
        }


        [Fact]
        public void When_calling_UpdateProject_passing_existing_project_it_changes_existingProject_inDB()
        {
            // Setup
            var existingProject = new Project
            {
                Id = 1
            };

            repoMock
                .Setup(r => r.Get(existingProject.Id))
                .Returns(existingProject);
            repoMock
                .Setup(r => r.Update(existingProject));
            repoMock
                .Setup(r => r.Add(existingProject));

            // Act
            projectList.UpdateProject(existingProject);

            // Assert
            repoMock.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
            repoMock.Verify(r => r.Update(existingProject), Times.Once);
            repoMock.Verify(r => r.Add(It.IsAny<Project>()), Times.Never);
        }


        [Fact]
        public void When_getting_projectWithId_returns_The_Project_With_Given_Id()
        {
            var existingProject = new Project
            {
                Id = 1
            };

            repoMock
                .Setup(r => r.Get(existingProject.Id))
                .Returns(existingProject);

            var result = projectList.GetById(existingProject.Id);

            Assert.Equal(existingProject, result);
        }


        private List<ProjectReport> CreateProjectReport() => new List<ProjectReport>()
        {
            new ProjectReport()
            {
                Project = new Project{OrderNumber = 2},
                Votes = 2
            },

            new ProjectReport()
            {
                Project = new Project{OrderNumber = 0},
                Votes = 1
            },

            new ProjectReport()
            {
                Project = new Project{OrderNumber = 1},
                Votes = 63
            }
        };


        [Fact]
        public void When_getting_projects_And_Campaign_Url_Model_AndCampaign_IsActive_returns_report_ordered_by_projectOrderNumber()
        {
            var activeCampaign = new Campaign()
            {
                Id = 1,
                StartDateTime = DateTime.Now.AddDays(-4),
                EndDateTime = DateTime.Now.AddDays(5)
            };
            
            repoMock
                .Setup(r => r.GetProjectsReport(activeCampaign.Id))
                .Returns(CreateProjectReport());

            //Act
            var model = projectList.GetProjectsAndCampaignUrlModel(activeCampaign);

            //Assert
            Assert.True(model.IsCampaignActive);

            Assert.Equal(0, model.Projects[0].Project.OrderNumber);
            Assert.Equal(1, model.Projects[1].Project.OrderNumber);
            Assert.Equal(2, model.Projects[2].Project.OrderNumber);
        }


        [Fact]
        public void When_getting_projects_And_Campaign_Url_Model_AndCampaign_IsNotActive_returns_report_ordered_by_Votes()
        {
            var nonActiveCampaign = new Campaign()
            {
                Id = 1,
                StartDateTime = DateTime.Now.AddDays(-4),
                EndDateTime = DateTime.Now.AddDays(-2)
            };

            repoMock
                .Setup(r => r.GetProjectsReport(nonActiveCampaign.Id))
                .Returns(CreateProjectReport());

            //Act
            var model = projectList.GetProjectsAndCampaignUrlModel(nonActiveCampaign);

            //Assert
            Assert.False(model.IsCampaignActive);

            Assert.Equal(63, model.Projects[0].Votes);
            Assert.Equal(2, model.Projects[1].Votes);
            Assert.Equal(1, model.Projects[2].Votes);
        }
    }
}
