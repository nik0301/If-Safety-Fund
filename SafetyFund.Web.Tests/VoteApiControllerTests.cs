using SafetyFund.Web.Controllers;
using Xunit;

namespace SafetyFund.Web.Tests
{
    public class VoteApiControllerTests
    {
        [Fact]
        public void When_creating_new_VoteInfo_object_its_parameters_equal_constructor_parameters()
        {
            var voteInfo = new VoteInfo(true, 1);

            Assert.True(voteInfo.IsVoteSuccesful);
            Assert.Equal(1, voteInfo.ProjectId);
        }


        [Fact]
        public void When_getting_hash_result_is_32symbols_long()
        {
            Assert.Equal(32, VoteApiController.GetMd5Hash("testing string").Length);
        }
    }
}
