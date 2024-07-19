using Microsoft.AspNetCore.Identity;

namespace StockWebApp1.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime DateCreated { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<User> Subscriptions { get; set; }
        public ICollection<User> Subscribers { get; set; }
    }
}
