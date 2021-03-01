using IFLike.DAL.Context;
using IFLike.DAL.Implementation;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IFLike.DAL.Interfaces
{
    public class PollRepository : RepositoryBase<Poll, int>, IPollRepository
    {
        public PollRepository(IFLikeContext context)
            :base(context)
        {
        }

        public override Poll GetById(int id)
        {
            return Context.Polls.Include(e => e.PollItems).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Poll> All()
        {
            return Context.Polls;
        }
    }
}
