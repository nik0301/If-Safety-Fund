using System;
using System.Collections.Generic;

namespace IFLike.Dto
{
    public class PollDto : DtoBaseId<int>
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public string Message { get; set; }

        public bool ShowMessage
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Message);
            }
        }

        public virtual List<PollItemDto> PollItems { get; set; } = new List<PollItemDto>();
    }
}
