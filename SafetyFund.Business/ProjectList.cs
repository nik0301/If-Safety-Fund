using System;
using SafetyFund.Business.Models;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using SafetyFund.Data.Reports;

namespace SafetyFund.Business
{
    public class ProjectList
    {
        private readonly ProjectRepo repo;


        public ProjectList(ProjectRepo repo)
        {
            this.repo = repo;
        }


        public virtual Project GetById(int id)
        {
            return repo.Get(id);
        }


        public virtual Project InitializeProjectForCampaign(int campaignId) => new Project
        {
            CampaignId = campaignId
        };


        public void UpdateProject(Project project)
        {
            if (repo.Get(project.Id) != null)
            {
                repo.Update(project);
            }
            else
            {
                repo.Add(project);
            }
        }


        public int DeleteProjectAndGetCampaignId(int id)
        {
            var project = repo.Get(id);
            repo.Delete(project);

            return project.CampaignId;
        }


        public ProjectsAndCampaignUrlModel GetProjectsAndCampaignUrlModel(Campaign campaign)
        {
            var model = new ProjectsAndCampaignUrlModel();
            var projects = repo.GetProjectsReport(campaign.Id);

            model.IsCampaignActive = campaign.StartDateTime <= DateTime.Now && campaign.EndDateTime >= DateTime.Now;

            model.Projects = model.IsCampaignActive
                ? projects.OrderBy(item => item.Project.OrderNumber).ToList()
                : projects.OrderByDescending(p => p.Votes).ToList();

            return model;
        }


        public virtual List<ProjectReport> GetProjectsWithVotesCount(int campaignId, DateTime campaignEndDate)
        {
            var isCampaignActive = campaignEndDate > DateTime.Now;

            return isCampaignActive
                ? repo.GetProjectsReport(campaignId).OrderBy(item => item.Project.OrderNumber).ToList()
                : repo.GetProjectsReport(campaignId).OrderByDescending(item => item.Votes).ToList();
        }
    }
}