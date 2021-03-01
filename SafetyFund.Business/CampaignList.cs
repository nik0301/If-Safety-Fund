using System.Collections.Generic;
using SafetyFund.Data.Models;
using SafetyFund.Data.Reports;
using SafetyFund.Data.Repositories;

namespace SafetyFund.Business
{
    public class CampaignList
    {
        private readonly CampaignRepo repo;


        public CampaignList(CampaignRepo repo)
        {
            this.repo = repo;
        }


        public IList<Campaign> Get()
        {
            return repo.Get();
        }


        public void Add(Campaign campaign)
        {
            repo.Add(campaign);
        }


        public void DeleteById(int id)
        {
            repo.Delete(repo.Get(id));
        }


        public void Update(Campaign campaign)
        {
            repo.Update(campaign);
        }


        public virtual Campaign Get(int id)
        {
            return repo.Get(id);
        }


        public virtual List<CampaignReport> GetCampaignsWithProjectCounts()
        {
            return repo.GetCampaignsAndProjectCounts();
        }


        public Campaign GetLastCampaign()
        {
            return repo.GetNewestCampaign();
        }
    }
}
