using System.Collections.Generic;
using SafetyFund.Data.Reports;

namespace SafetyFund.Business.Models
{
    public class ProjectsAndCampaignUrlModel
    {
        public List<ProjectReport> Projects { get; set; }
        public bool IsCampaignActive { get; set; }
    }
}
