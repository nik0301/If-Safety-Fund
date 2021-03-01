using Microsoft.AspNetCore.Mvc;

namespace SafetyFund.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}