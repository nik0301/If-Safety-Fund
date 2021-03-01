using Microsoft.AspNetCore.Mvc;
using Moq;
using SafetyFund.Business;
using SafetyFund.Data.Models;
using SafetyFund.Data.Tests;
using SafetyFund.Web.Controllers;
using Xunit;

namespace SafetyFund.Web.Tests
{
    public class UserControllerTests : AbstractBaseTests
    {
        private readonly Mock<UserList> userListMock;
        private readonly UserController controller;

        public UserControllerTests()
        {
            userListMock = new Mock<UserList>(null);
            controller = new UserController(userListMock.Object);
        }


        [Fact]
        public void When_In_createEdit_passed_existing_Object_Data_UserEditViewModel_Retrieves_Existing_User_From_Db_And_Method_Returns_NotNull()
        {
            //Setup
            userListMock
                .Setup(p => p.Get(It.IsAny<string>()))
                .Returns((User)null);
            //Act
            var result = controller.CreateEdit("User150") as IActionResult;
            //Assert
            Assert.NotNull(result);
            userListMock.Verify(p => p.Get(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void When_Calling_index_then_return_Index()
        {//setup
            userListMock
                .Setup(p => p.Get());
            //act
            var result = controller.Index() as ViewResult;
            //assert
            Assert.NotNull(result);
        }


        [Fact]
        public void When_In_createEdit_passed_notExisting_Object_Data_UserEditViewModel_JustCreates_NewUser_And__Method_Returns_NotNull()
        {
            // Setup
            userListMock
                .Setup(p => p.Get(It.IsAny<string>()))
                .Returns((User)null);

            // Act
            var result = controller.CreateEdit((string)null);

            // Assert
            Assert.NotNull(result);
            userListMock.Verify(p => p.Get(It.IsAny<string>()), Times.Never);
        }
    }
}