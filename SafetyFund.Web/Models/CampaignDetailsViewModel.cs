using System.Collections.Generic;
using SafetyFund.Data.Models;
using SafetyFund.Data.Reports;

namespace SafetyFund.Web.Models
{
    public class CampaignDetailsViewModel
    {
        public Campaign Campaign { get; set; }
        public List<ProjectReport> Projects { get; set; }
    }
}
