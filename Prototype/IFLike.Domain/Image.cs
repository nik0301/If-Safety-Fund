using System;
using System.Collections.Generic;
using System.Text;

namespace IFLike.Domain
{
    public class Image : EntityBaseId<int>
    {
        public int PollItemId { get; set; }
        public byte[] Content { get; set; }
        public bool IsDefault { get; set; }
        public string FileName { get; set; }
        public PollItem PollItem { get; set; }
    }
}
