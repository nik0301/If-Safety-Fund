using System;

namespace IFLike.Domain
{
    public class PollResult : EntityBaseId<int>
    {
        public string UserEmail { get; set; }

        public int PollItemId { get; set; }

        public string CountryCode { get; set; }

        public DateTime Created { get; set; }

        public PollItem PollItem { get; set; }
    }
}