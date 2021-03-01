using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SafetyFund.Business;

namespace SafetyFund.Web.Controllers.Authentication
{
    public class SafetyFundAuthorizeAttribute : TypeFilterAttribute
    {
        public SafetyFundAuthorizeAttribute()
            : base(typeof(AuthorizeActionFilter))
        {
        }

        private class AuthorizeActionFilter : IAsyncActionFilter
        {
            private readonly Autorization autorization;

            public AuthorizeActionFilter(Autorization autorization)
            {
                this.autorization = autorization;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var isAuthorized = autorization.IsAuthorized(context.HttpContext.User.Identity.Name);

                if (!isAuthorized)
                {
                    context.Result = new RedirectToActionResult("Index", "Error", null);
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
