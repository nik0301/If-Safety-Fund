using System;
using System.Collections.Generic;
using System.Text;

namespace IFLike.Dto
{
    public class ImageDto
    {
        public int Id { get; set; }
        public int PollItemId { get; set; }
        public byte[] Content { get; set; }
        public bool IsDefault { get; set; }
        public string FileName { get; set; }
        public PollItemDto PollItem { get; set; }
    }
}
