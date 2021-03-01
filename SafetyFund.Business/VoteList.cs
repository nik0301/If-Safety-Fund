using System;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;

namespace SafetyFund.Business
{
    public class VoteList
    {
        private readonly VoteRepo repo;


        public VoteList(VoteRepo repo)
        {
            this.repo = repo;
        }


        public virtual void AddVote(int projectId, string userId, string socialName)
        {
            socialName = socialName.ToLower();

            if (socialName != "facebook"
                && socialName != "draugiem")
            {
                throw new InvalidSocialMediaException();
            }

            if (repo.HasUserVotedToday(userId, projectId, socialName))
            {
                throw new UserAlreadyVotedException();
            }

            var vote = new Vote
            {
                UserId = userId,
                ProjectId = projectId,
                VotingDateTime = DateTime.Now,
                SocialName = socialName
            };

            repo.Add(vote);
        }


        public virtual int GetVotesCountByProject(int projectId)
        {
            return repo.GetVotesCountByProject(projectId);
        }
    }
}