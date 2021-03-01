using IFLike.DAL.Interfaces;
using IFLike.Domain;
using IFLike.Services.Interfaces;

namespace IFLike.Services.Implementation
{
    public class PollItemService : IPollItemService
    {
        private readonly IPollItemRepository _pollItemRepository;

        public PollItemService(IPollItemRepository pollItemRepository)
        {
            _pollItemRepository = pollItemRepository;
        }

        public PollItem Find(int id)
        {
            return _pollItemRepository.GetById(id);
        }
    }
}
