using Microsoft.AspNetCore.Mvc;
using Moq;
using SafetyFund.Business;
using SafetyFund.Data.Models;
using SafetyFund.Data.Tests;
using SafetyFund.Web.Controllers;
using Xunit;

namespace SafetyFund.Web.Tests
{
    public class ProjectControllerTests : AbstractBaseTests
    {
        private readonly Mock<ProjectList> projectListMock;
        private readonly ProjectController controller;


        public ProjectControllerTests()
        {
            projectListMock = new Mock<ProjectList>(null);
            controller = new ProjectController(projectListMock.Object);
        }


        [Fact]
        public void When_In_createEdit_passed_existing_Object_Data_ProjectEditViewModel_Retrieves_Existing_Project_From_Db_And_Method_Returns_NotNull()
        {
            // Setup
            projectListMock
                .Setup(p => p.GetById(It.IsAny<int>()))
                .Returns((Project)null);

            // Act
            var result = controller.CreateEdit(1, 1) as IActionResult;

            // Assert
            Assert.NotNull(result);
            projectListMock.Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public void When_In_createEdit_passed_notExisting_Object_Data_ProjectEditViewModel_JustCreates_NewProject_And__Method_Returns_NotNull()
        {
            // Setup
            projectListMock
                .Setup(p => p.InitializeProjectForCampaign(It.IsAny<int>()))
                .Returns((Project)null);

            // Act
            var result = controller.CreateEdit(1, null) as IActionResult;

            // Assert
            Assert.NotNull(result);
            projectListMock.Verify(p => p.InitializeProjectForCampaign(It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public void When_Image_Data_Not_Null_GetImage_Returns_NotNull()
        {
            // Setup
            var project = new Project()
            {
                Image = new byte[] { 200, 200, 200, 200 }
            };

            projectListMock
                .Setup(p => p.GetById(It.IsAny<int>()))
                .Returns(project);

            // Act
            var result = controller.GetImage(1);

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void When_Image_Data_Null_GetImage_Returns_Null()
        {
            // Setup
            var project = new Project()
            {
                Image = null
            };

            projectListMock
                .Setup(p => p.GetById(It.IsAny<int>()))
                .Returns(project);

            // Act
            var result = controller.GetImage(1);

            // Assert
            Assert.Null(result);
        }
    }
}