namespace SafetyFund.Web.Controllers
{
    public class VoteInfo
    {
        public bool IsVoteSuccesful { get; set; }
        public int ProjectId { get; set; }

        public VoteInfo(bool isVoteSuccesful, int projectId)
        {
            IsVoteSuccesful = isVoteSuccesful;
            ProjectId = projectId;
        }
    }
}