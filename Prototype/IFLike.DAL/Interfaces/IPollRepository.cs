using System.Collections.Generic;
using IFLike.Domain;

namespace IFLike.DAL.Interfaces
{
    public interface IPollRepository : IRepositoryBase<Poll, int>
    {
        IEnumerable<Poll> All();

    }
}
