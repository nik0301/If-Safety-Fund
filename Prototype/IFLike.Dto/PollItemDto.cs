using System.Collections.Generic;
using System.Linq;

namespace IFLike.Dto
{
    public class PollItemDto : DtoBaseId<int>
    {
        public int ImageId
        {
            get { return Images.FirstOrDefault(i => i.IsDefault)?.Id ?? 0; }
        }

        public string ImageFileName => Images.FirstOrDefault(i => i.IsDefault)?.FileName ?? "";

        public int PollId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int VotesCount { get; set; }
        public List<ImageDto> Images { get; set; } = new List<ImageDto>();

    }
}
