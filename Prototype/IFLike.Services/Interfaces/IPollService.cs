using IFLike.Dto;

namespace IFLike.Services.Interfaces
{
    public interface IPollService
    {
        PollDto GetById(int id);
    }
}
