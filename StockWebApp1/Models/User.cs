namespace StockWebApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<User> Subscriptions { get; set; }
        public ICollection<User> Subscribers { get; set; }
    }
}
