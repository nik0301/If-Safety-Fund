using System.Collections.Generic;
using Moq;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using SafetyFund.Data.Tests;
using Xunit;

namespace SafetyFund.Business.Tests
{
    public class UserListTests : AbstractBaseTests
    {
        private readonly UserList userList;
        private readonly Mock<UserRepo> repoMock;


        public UserListTests()
        {
            repoMock = new Mock<UserRepo>(null);
            userList = new UserList(repoMock.Object);
        }


        [Fact]
        public void When_user_with_given_ID_existsInDB_adding_new_user_with_same_Id_throws_UserAlreadyExistsException()
        {
            var existingId = "abc";

            repoMock
                .Setup(r => r.Get(existingId))
                .Returns(new User
                {
                    Id = existingId
                });

            var newUser = new User { Id = existingId };

            Assert.Throws<UserAlreadyExistsException>(() => userList.UpdateUser(newUser, false));
            repoMock.Verify(r => r.Get(existingId), Times.Once);
        }


        [Fact]
        public void When_User_With_Given_Id_notExist_InDb_NewUserWithGivenId_Added_ToDb()
        {
            repoMock
                .Setup(r => r.Get(It.IsAny<string>()))
                .Returns((User)null);
            repoMock
                .Setup(r => r.Add(It.IsAny<User>()));


            var newUser = new User
            {
                Id = "abc"
            };

            userList.UpdateUser(newUser, false);

            repoMock.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        }


        [Fact]
        public void When_User_With_Given_Id_Exist_InDb_ExistingUserWithGivenId_IsEdited_InDb()
        {
            var user = new User()
            {
                Id = "abc"
            };

            repoMock
                .Setup(r => r.Get(It.IsAny<string>()))
                .Returns(user);
            repoMock
                .Setup(r => r.Update(It.IsAny<User>()));


            userList.UpdateUser(user, true);

            repoMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }


        [Fact]
        public void When_add_user_and_deleteById_then_return_null()
        {
            //Setup
            var user = new User()
            {
                Id = "hhhhhh"
            };

            repoMock
                .Setup(r => r.Get(It.IsAny<string>()))
                .Returns(user);
            repoMock
                .Setup(r => r.Delete(It.IsAny<User>()));

            //Act
            userList.DeleteById("hhhhhh");
            //Assert
            repoMock.Verify(r=>r.Delete(It.IsAny<User>()),Times.Once);

        }


        [Fact]
        public void When_get_then_result_is_list_ofUsers()
        {
            //Setup
            var users = new List<User>
            {
                new User()
                {
                    Id = "Korzh"
                },

                new User()
                {
                    Id = "Nice"
                }

            };

            repoMock
                .Setup(r => r.Get())
                .Returns(users);
            //Act

            var userfromDb = userList.Get();
            //Assert

            Assert.Equal(users, userfromDb);
        }


        [Fact]
        public void When_getting_user_by_Id_It_Returns_User_With_Given_Id()
        {
            //Setup
            var user = new User
            {
                Id = "europe/ty"
            };

            repoMock
                .Setup(r => r.Get(user.Id))
                .Returns(user);

            //Act
            var returnedUser = userList.Get(user.Id);

            //Assert
            Assert.Equal(user.Id, returnedUser.Id);
        }       
    }
}
