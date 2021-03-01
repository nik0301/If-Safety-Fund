using SafetyFund.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SafetyFund.Data.Reports;

namespace SafetyFund.Data.Repositories
{
    public class ProjectRepo : AbstractRepo<Project, int>
    {
        public ProjectRepo(DbContextOptions options) : base(options)
        {
        }


        public virtual List<Project> GetByCampaign(int campaignId)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                return db.Projects.Where(project => project.CampaignId == campaignId).ToList();
            }
        }


        public virtual List<ProjectReport> GetProjectsReport(int campaignId)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                var query = from project in db.Projects
                            where project.CampaignId == campaignId
                            join vote in db.Votes on project.Id equals vote.ProjectId into v
                            from joinedVote in v.DefaultIfEmpty()
                            group joinedVote by project
                    into projectGroup
                            select new ProjectReport
                            {
                                Project = projectGroup.Key,
                                Votes = projectGroup.Count(p => p != null)
                            };

                return query.ToList();
            }
        }
    }
}
