using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafetyFund.Business;
using SafetyFund.Data.Models;
using SafetyFund.Web.Controllers.Authentication;

namespace SafetyFund.Web.Controllers
{
    [Authorize]
    [SafetyFundAuthorize]
    public class LocationController : Controller
    {
        private readonly LocationList locationList;
        public LocationController(LocationList locationList)
        {
            this.locationList = locationList;
        }


        // GET: Location
        public IActionResult Index()
        {
            var locations = locationList.Get();

            return View(locations);
        }


        public IActionResult Delete(int id)
        {
            locationList.DeleteById(id);
            return RedirectToAction("Index");
        }


        public ActionResult CreateEdit(int? id)
        {
            return View(id.HasValue ? locationList.Get(id.Value) : new Location());
        }


        [HttpPost]
        public ActionResult CreateEdit(Location location)
        {
            if (ModelState.IsValid)
            {
                if (location.Id > 0)
                {
                    locationList.Update(location);
                }
                else
                {
                    locationList.Add(location);
                }
                return RedirectToAction("Index");
            }
            return View(nameof(CreateEdit), location);
        }       
    }
}