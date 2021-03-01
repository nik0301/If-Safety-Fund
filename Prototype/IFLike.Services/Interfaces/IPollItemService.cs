using IFLike.Domain;

namespace IFLike.Services.Interfaces
{
    public interface IPollItemService
    {
        PollItem Find(int id);
    }
}
