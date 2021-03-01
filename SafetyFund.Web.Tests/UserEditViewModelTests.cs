using SafetyFund.Data.Models;
using SafetyFund.Web.Models;
using Xunit;

namespace SafetyFund.Web.Tests
{
    public class UserEditViewModelTests
    {
        [Fact]
        public void When_user_existsAlready_then_UserEditViewModeL_constructor_doesnt_Create_newUser_and_ID_starSymbols_are_replacedBy_Backslashes()
        {
            var user = new User
            {
                Id = "europe*test",
                FullName = "Testing user"
            };

            var result = new UserEditViewModel(user);

            Assert.True(result.IsExisting);
            Assert.Equal("europe*test", result.Id);
            Assert.Equal(result.User, user);
            Assert.Equal("europe\\test", result.User.Id);
        }


        [Fact]
        public void When_creating_new_UserEditViewModeL_passing_no_values_then_model_isEmpty()
        {
            var userEvm = new UserEditViewModel();

            Assert.Null(userEvm.Id);
            Assert.Null(userEvm.User);
            Assert.Null(userEvm.ErrorMessage);
        }
    }
}
