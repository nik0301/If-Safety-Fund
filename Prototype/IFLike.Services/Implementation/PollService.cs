using IFLike.Services.Interfaces;
using IFLike.Dto;
using IFLike.DAL.Interfaces;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IFLike.Services.Implementation
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _pollRepository;
        private readonly IPollResultRepository _pollResultRepository;
        private readonly IImageRepository _imageRepository;

        public PollService(IPollRepository pollRepository, IPollResultRepository pollResultRepository, IImageRepository imageRepository)
        {
            _pollRepository = pollRepository;
            _pollResultRepository = pollResultRepository;
            _imageRepository = imageRepository;
        }

        public PollDto GetById(int id)
        {
            var poll = _pollRepository.GetById(id);
            return new PollDto()
            {
                Header = poll.Header,
                Name = poll.Name,
                Description = poll.Description,
                PollItems = poll.PollItems.Select(pi => new PollItemDto()
                {
                    Id = pi.Id,
                    Name = pi.Name,
                    Description = pi.Description,
                    VotesCount = _pollResultRepository.GetVotesCount(pi.Id),
                    Images = _imageRepository.GetByPollItemId(pi.Id).Select(i => new ImageDto()
                    {
                        Id = i.Id,
                        FileName = i.FileName,
                        IsDefault =  i.IsDefault
                    }).ToList()
                })
                .ToList()
            };
        }
    }
}
