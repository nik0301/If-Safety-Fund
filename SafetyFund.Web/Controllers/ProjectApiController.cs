using Microsoft.AspNetCore.Mvc;
using SafetyFund.Business;

namespace SafetyFund.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/ProjectApi")]
    public class ProjectApiController : Controller
    {
        private readonly ProjectList projectList;
        private readonly LocationService locationService;
        private readonly CampaignList campaignList;

        public ProjectApiController(CampaignList campaignList, ProjectList projectList, LocationService locationService)
        {
            this.campaignList = campaignList;
            this.projectList = projectList;
            this.locationService = locationService;
        }


        // GET: api/ProjectApi/GetProjectsByCampaignId
        [Route("GetLastCampaignProjects")]
        [HttpGet]
        public IActionResult GetLastCampaignProjects()
        {
            if (!locationService.IsCountryAllowed(Request.HttpContext.Connection.RemoteIpAddress))
            {
                return Forbid();
            }

            var lastCampaign = campaignList.GetLastCampaign();

            return Ok(projectList.GetProjectsAndCampaignUrlModel(lastCampaign));
        }
    }
}
