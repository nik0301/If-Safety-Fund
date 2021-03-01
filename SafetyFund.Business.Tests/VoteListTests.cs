using Moq;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using SafetyFund.Data.Tests;
using Xunit;

namespace SafetyFund.Business.Tests
{
    public class VoteListTests : AbstractBaseTests
    {
        private readonly VoteList voteList;
        private readonly Mock<VoteRepo> repoMock;


        public VoteListTests()
        {
            repoMock = new Mock<VoteRepo>(null);
            voteList = new VoteList(repoMock.Object);
        }


        [Fact]
        public void When_user_has_voted_today_and_user_want_vote_at_same_day_same_project_then_throw_new_UserAlreadyVoted_exception()
        {
            repoMock
                .Setup(r => r.HasUserVotedToday(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true);
            repoMock
                .Setup(r => r.Add(It.IsAny<Vote>()));

            Assert.Throws<UserAlreadyVotedException>(() => voteList.AddVote(2, "2", "draugiem"));
            repoMock.Verify(r => r.Add(It.IsAny<Vote>()), Times.Never);
        }


        [Fact]
        public void When_user_dont_voted_today_and_user_press_vote_button_then_add_new_record_in_DB()
        {
            repoMock
                .Setup(r => r.HasUserVotedToday(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(false);
            repoMock
                .Setup(r => r.Add(It.IsAny<Vote>()));

            // Act
            voteList.AddVote(2, "2", "draugiem");

            repoMock.Verify(r => r.Add(It.IsAny<Vote>()), Times.Once);
        }


        [Fact]
        public void When_user_want_try_vote_from_something_else_not_facebook_or_draugiem_then_throw_new_Invalid_Social_Media_Exception()
        {
            repoMock
                .Setup(r => r.HasUserVotedToday(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(false);
            repoMock
                .Setup(r => r.Add(It.IsAny<Vote>()));

            Assert.Throws<InvalidSocialMediaException>(() => voteList.AddVote(2, "2", "blblvlflflf"));
            repoMock.Verify(r=>r.Add(It.IsAny<Vote>()),Times.Never);
        }


        [Fact]
        public void When_getting_votes_Count_ByProject_GetVotesCountByProject_returns_right_votesCount()
        {
            //Setup
            const int testingVotesCount = 2;
            const int testingProjectId = 1;

            repoMock
                .Setup(r => r.GetVotesCountByProject(testingProjectId))
                .Returns(testingVotesCount);

            //Act
            var result = voteList.GetVotesCountByProject(testingProjectId);

            //Assert
            Assert.Equal(testingVotesCount, result);
            repoMock.Verify(r => r.GetVotesCountByProject(testingProjectId), Times.Once);
        }
    }
}
