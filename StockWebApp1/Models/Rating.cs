namespace StockWebApp1.Models
{
    public class Rating : BaseModel
    {
        public bool IsLiked { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }  
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
    }
}
