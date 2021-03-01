using IFLike.Domain;
using IFLike.DAL.Context;
using System.Linq;
using IFLike.DAL.Interfaces;

namespace IFLike.DAL.Implementation
{
    public class PollResultRepository : RepositoryBase<PollResult, int>, IPollResultRepository
    {
        public PollResultRepository(IFLikeContext iFLikeContext) : base(iFLikeContext)
        {
        }

        public PollResult GetBy(string email, int pollItemId)
        {
            return Context.PollResults.FirstOrDefault(x => x.PollItemId == pollItemId && x.UserEmail == email);
        }

        public int GetVotesCount(int pollItemId)
        {
            return Context.PollResults.Where(pr => pr.PollItemId == pollItemId).Count();
        }
    }
}
