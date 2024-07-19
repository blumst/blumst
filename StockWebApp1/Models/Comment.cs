namespace StockWebApp1.Models
{
    public class Comment : BaseModel
    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
    }
}
