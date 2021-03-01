using IFLike.DAL.Interfaces;
using IFLike.Domain;
using IFLike.DAL.Context;

namespace IFLike.DAL.Implementation
{
    public class PollItemRepository : RepositoryBase<PollItem, int>, IPollItemRepository
    {
        public PollItemRepository(IFLikeContext iFLikeContext) : base(iFLikeContext)
        {
        }
    }
}