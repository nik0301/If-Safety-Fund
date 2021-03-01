using System;
using System.Collections.Generic;
using System.Text;

namespace IFLike.Dto
{
    public class PollEditDto : DtoBaseId<int>
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<PollItemDto> PollItems { get; set; } = new List<PollItemDto>();
    }
}
