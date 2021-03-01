using System.Collections.Generic;
using SafetyFund.Data.Models;

namespace SafetyFund.Web.Models
{
    public class ProjectIndexViewModel
    {
        public List<Project> Projects { get; set; }
        public int CampaignId { get; set; }


        public ProjectIndexViewModel(List<Project> projects, int campaignId)
        {
            CampaignId = campaignId;
            Projects = projects;
        }
    }
}
