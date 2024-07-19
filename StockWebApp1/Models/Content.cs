namespace StockWebApp1.Models
{
    public class Content : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<ContentTag> ContentTags { get; set; }
    }
}

