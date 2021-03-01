using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafetyFund.Business;
using SafetyFund.Data.Models;
using SafetyFund.Web.Controllers.Authentication;
using SafetyFund.Web.Models;

namespace SafetyFund.Web.Controllers
{
    [Authorize]
    [SafetyFundAuthorize]
    public class HomeController : Controller
    {
        private readonly CampaignList campaignList;
        private readonly ProjectList projectList;


        public HomeController(CampaignList campaign, ProjectList project)
        {
            this.campaignList = campaign;
            this.projectList = project;
        }


        public IActionResult Index()
        {
            return View(campaignList.GetCampaignsWithProjectCounts());
        }


        public IActionResult Delete(int id)
        {
            campaignList.DeleteById(id);
            return RedirectToAction("Index");
        }



        public ActionResult Details(int id)
        {
            var campaign = campaignList.Get(id);

            return View(new CampaignDetailsViewModel
            {
                Campaign = campaign,
                Projects = projectList.GetProjectsWithVotesCount(id, campaign.EndDateTime)
            });
        }


        public ActionResult CreateEdit(int? id)
        {
            return View(id.HasValue ? campaignList.Get(id.Value) : new Campaign());
        }


        [HttpPost]
        public ActionResult CreateEdit(Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                if (campaign.Id > 0)
                {
                    campaignList.Update(campaign);
                }
                else
                {
                    campaignList.Add(campaign);
                }
                return RedirectToAction("Index");
            }
            return View(nameof(CreateEdit), campaign);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
