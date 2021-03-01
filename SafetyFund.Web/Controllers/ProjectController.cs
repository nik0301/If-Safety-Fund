using Microsoft.AspNetCore.Mvc;
using SafetyFund.Business;
using SafetyFund.Web.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using SafetyFund.Web.Controllers.Authentication;

namespace SafetyFund.Web.Controllers
{
    [Authorize]
    [SafetyFundAuthorize]
    public class ProjectController : Controller
    {
        private readonly ProjectList projectList;


        public ProjectController(ProjectList projectList)
        {
            this.projectList = projectList;
        }


        public ActionResult Details(int id)
        {
            return View(projectList.GetById(id));
        }


        public ActionResult CreateEdit(int campaignId, int? id)
        {
            var projectVm = new ProjectEditViewModel
            {
                Project = id.HasValue
                ? projectList.GetById(id.Value)
                : projectList.InitializeProjectForCampaign(campaignId)
            };

            return View(projectVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit(ProjectEditViewModel projectVM)
        {
            if (!ModelState.IsValid)
            {
                return View(projectVM);
            }

            if (projectVM.FeaturedImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    projectVM.FeaturedImage.CopyToAsync(memoryStream);
                    projectVM.Project.Image = memoryStream.ToArray();
                }
            }

            projectList.UpdateProject(projectVM.Project);

            return RedirectToAction("Details", "Home", new { id = projectVM.Project.CampaignId });
        }


        public FileContentResult GetImage(int id)
        {
            var img = projectList.GetById(id).Image;

            return (img != null)
                ? File(img, "image/jpg")
                : null;
        }

     
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Details", "Home", new { id = projectList.DeleteProjectAndGetCampaignId(id) });
        }
    }
}