namespace IFLike.Dto
{
    public class PollCreateDto
    {
        public PollDto Poll { get; set; }
        public ImageDto Image { get; set; }
        public PollItemDto Item { get; set; }
    }
}