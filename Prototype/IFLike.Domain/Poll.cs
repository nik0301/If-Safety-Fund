using System;
using System.Collections.Generic;
using System.Text;

namespace IFLike.Domain
{
    public class Poll : EntityBaseId<int>
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }

        public virtual List<PollItem> PollItems { get; set; }
    }
}
