namespace StockWebApp1.Models
{
    public class Content
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}

