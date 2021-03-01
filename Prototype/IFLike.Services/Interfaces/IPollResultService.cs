namespace IFLike.Services.Interfaces
{
    public interface IPollResultService
    {
        bool AddVote(string email, int pollItemId, string countryCode);

        int GetVoteCount(int pollResultId);
    }
}
