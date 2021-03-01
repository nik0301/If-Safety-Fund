using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafetyFund.Business;
using SafetyFund.Web.Controllers.Authentication;
using SafetyFund.Web.Models;

namespace SafetyFund.Web.Controllers
{
    [Authorize]
    [SafetyFundAuthorize]
    public class UserController : Controller
    {
        private readonly UserList userList;


        public UserController(UserList user)
        {
            this.userList = user;
        }


        public IActionResult Index()
        {
            var users = userList.Get();

            return View(users);
        }


        public IActionResult Delete(string id)
        {
            userList.DeleteById(id.Replace('*', '\\'));
            return RedirectToAction("Index");
        }


        public ActionResult CreateEdit(string identity)
        {
            var userEditVM = new UserEditViewModel(
                (identity != null)
                    ? userList.Get(identity.Replace('*', '\\'))
                    : null
                );

            return View(userEditVM);
        }


        [HttpPost]
        public ActionResult CreateEdit(UserEditViewModel userEditVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userEditVM.HasErrors = false;
                    userList.UpdateUser(userEditVM.User, userEditVM.IsExisting);
                }
                catch (UserAlreadyExistsException)
                {
                    userEditVM.HasErrors = true;
                    userEditVM.ErrorMessage = "Cannot add user with given ID, because user with given ID already exists.";
                    return View(nameof(CreateEdit), userEditVM);
                }

                return RedirectToAction("Index");
            }
            return View(nameof(CreateEdit), userEditVM);
        }

    }
}