using SafetyFund.Data.Models;

namespace SafetyFund.Web.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        public User User { get; set; }
        public bool IsExisting { get; set; }
        public bool HasErrors { get; set; }
        public string ErrorMessage { get; set; }

        public UserEditViewModel()
        {
        }


        public UserEditViewModel(User user)
        {
            HasErrors = false;

            if (user != null)
            {
                IsExisting = true;
                Id = user.Id;

                User = user;
                User.Id = User.Id.Replace('*', '\\');
            }
            else
            {
                IsExisting = false;
                User = new User();
            }

        }
    }
}
