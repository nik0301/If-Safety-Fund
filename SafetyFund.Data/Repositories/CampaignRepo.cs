using System;
using Microsoft.EntityFrameworkCore;
using SafetyFund.Data.Models;
using System.Linq;
using System.Collections.Generic;
using SafetyFund.Data.Reports;

namespace SafetyFund.Data.Repositories
{
    public class CampaignRepo : AbstractRepo<Campaign, int>
    {
        public CampaignRepo(DbContextOptions options) : base(options)
        {
        }


        public virtual Campaign GetNewestCampaign()
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                var campaignsOrderedByEndTime = db.Campaigns.OrderByDescending(item => item.EndDateTime);
                var lastActiveCampaign = campaignsOrderedByEndTime.First(item => item.EndDateTime > DateTime.Now && item.StartDateTime <= DateTime.Now);

                return lastActiveCampaign ?? campaignsOrderedByEndTime.First(item => item.StartDateTime < DateTime.Now);
            }
        }


        public virtual List<CampaignReport> GetCampaignsAndProjectCounts()
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                var query = from campaign in db.Campaigns
                            join project in db.Projects on campaign.Id equals project.CampaignId into proj
                            from joinedProject in proj.DefaultIfEmpty()
                            orderby campaign.EndDateTime descending
                            group joinedProject by campaign
                    into campaignGroup
                            select new CampaignReport()
                            {
                                Campaign = campaignGroup.Key,
                                ProjectCount = campaignGroup.Count(p => p != null)
                            };

                return query.ToList();
            }
        }
    }
}
