using System;
using System.Collections.Generic;
using System.Text;

namespace IFLike.Domain
{
    public class PollItem : EntityBaseId<int>
    {
        public int PollId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Poll Poll { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
    }
}
