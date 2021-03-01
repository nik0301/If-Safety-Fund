namespace IFLike.Domain
{
    public class Country : EntityBaseId<int>
    {
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public bool IsAllowed { get; set; }
        public int? MaxVoteCount { get; set; }
    }
}
