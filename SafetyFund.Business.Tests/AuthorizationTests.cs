using Moq;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using SafetyFund.Data.Tests;
using Xunit;

namespace SafetyFund.Business.Tests
{
    public class AuthorizationTests : AbstractBaseTests
    {
        private readonly Autorization autorization;
        private readonly Mock<UserRepo> repoMock;

        
        public AuthorizationTests()
        {
            repoMock = new Mock<UserRepo>(null);
            autorization = new Autorization(repoMock.Object);
        }


        [Fact]
        public void When_user_exist_in_DB_Then_user_is_autorized()
        {
            repoMock
                .Setup(r => r.Get(It.IsAny<string>()))
                .Returns(new User());

            var isAuthorized = autorization.IsAuthorized("test");

            Assert.True(isAuthorized);
        }


        [Fact]
        public void When_user_not_exist_in_DB_Then_user_is_not_autorized()
        {
            repoMock
                .Setup(r => r.Get(It.IsAny<string>()))
                .Returns((User)null);
            repoMock
                .Setup(r => r.GetUserCount())
                .Returns(1);

            var isAuthorized = autorization.IsAuthorized("test");

            Assert.False(isAuthorized);
        }


        [Fact]
        public void When_no_users_in_DB_Then_new_user_created_and_user_is_autorized()
        {
            // Setup
            repoMock
                .Setup(r => r.Get(It.IsAny<string>()))
                .Returns((User)null);
            repoMock
                .Setup(r => r.GetUserCount())
                .Returns(0);
            repoMock
                .Setup(r => r.Add(It.IsAny<User>()));

            // Act
            var isAuthorized = autorization.IsAuthorized("test");

            // Assert
            Assert.True(isAuthorized);
            repoMock.Verify(r => r.Add(It.Is<User>(u => u.Id == "test" && u.FullName == "test")), Times.Once);
        }
    }
}
