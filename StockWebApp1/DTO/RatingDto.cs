namespace StockWebApp1.DTO
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public bool IsLiked { get; set; }
        public Guid UserId { get; set; }
        public Guid ContentId { get; set; }
    }
}
