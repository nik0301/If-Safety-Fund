using IFLike.Domain;

namespace IFLike.DAL.Interfaces
{
    public interface IPollResultRepository : IRepositoryBase<PollResult, int>
    {
        PollResult GetBy(string email, int pollItemId);
        int GetVotesCount(int id);
    }
}