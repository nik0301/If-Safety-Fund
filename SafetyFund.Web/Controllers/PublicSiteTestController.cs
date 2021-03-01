using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafetyFund.Web.Controllers.Authentication;

namespace SafetyFund.Web.Controllers
{
    [Authorize]
    [SafetyFundAuthorize]
    public class PublicSiteTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}