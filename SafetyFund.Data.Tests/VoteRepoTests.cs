using System;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using Xunit;

namespace SafetyFund.Data.Tests
{
    public class VoteRepoTests : AbstractBaseTests
    {
        private readonly VoteRepo repo;

        public VoteRepoTests()
        {
            this.repo = new VoteRepo(GetDbOptions());
        }


        public Vote CreateVote(bool isAddedToDb = true)
        {
            var userId = Guid.NewGuid().ToString();

            var project = new ProjectRepoTests().CreateProject();

            var vote = new Vote
            {
                UserId = userId,
                ProjectId = project.Id,
                VotingDateTime = DateTime.Now,
                SocialName = "facebook"
            };

            if (isAddedToDb)
            {
                repo.Add(vote);
            }

            return vote;
        }


        [Fact]
        public void When_user_voted_today_for_project_HasUserVotedToday_returns_true()
        {
            var vote = CreateVote();
            var hasUserVotedToday = repo.HasUserVotedToday(vote.UserId, vote.ProjectId, vote.SocialName);
            Assert.True(hasUserVotedToday);
        }


        [Fact]
        public void When_user_didnt_vote_today_for_project_HasUserVotedToday_returns_false()
        {
            var vote = CreateVote(false);
            vote.VotingDateTime = vote.VotingDateTime.AddYears(-5);
            repo.Add(vote);

            var hasUserTodayVoted = repo.HasUserVotedToday(vote.UserId, vote.ProjectId, vote.SocialName);

            Assert.False(hasUserTodayVoted);
        }


        [Fact]
        public void When_getting_votes_count_of_project_GetVotesCountByProject_returns_right_value()
        {
            var projectWithVotes = new ProjectRepoTests().CreateProject();
            var projectWithoutVotes = new ProjectRepoTests().CreateProject();

            var vote = new Vote()
            {
                ProjectId = projectWithVotes.Id,
                SocialName = "facebook",
                UserId = Guid.NewGuid().ToString(),
                VotingDateTime = DateTime.Now
            };

            repo.Add(vote);

            //Act
            var projectWithoutVotesCount = repo.GetVotesCountByProject(projectWithoutVotes.Id);
            var projectWithVotesCount = repo.GetVotesCountByProject(projectWithVotes.Id);

            //Assert
            Assert.Equal(0, projectWithoutVotesCount);
            Assert.Equal(1, projectWithVotesCount);
        }
    }
}
