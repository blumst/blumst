namespace StockWebApp1.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public bool IsLiked { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}
