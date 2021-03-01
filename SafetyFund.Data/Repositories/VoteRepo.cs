using System;
using System.Linq;
using SafetyFund.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace SafetyFund.Data.Repositories
{
    public class VoteRepo : AbstractRepo<Vote, int>
    {
        public VoteRepo(DbContextOptions options) : base(options)
        {
        }


        public virtual bool HasUserVotedToday(string userId, int projectId, string socialName)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                return db.Votes.Any(item =>
                    item.UserId == userId
                    && item.ProjectId == projectId
                    && item.VotingDateTime.Date == DateTime.Today
                    && item.SocialName == socialName);
            }
        }


        public virtual int GetVotesCountByProject(int projectId)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                return db.Votes.Count(item => item.ProjectId == projectId);
            }
        }
    }
}
