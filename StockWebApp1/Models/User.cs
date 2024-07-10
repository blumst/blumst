namespace StockWebApp1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<Subscription> Subscribers { get; set; }
    }
}
