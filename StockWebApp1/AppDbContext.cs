using Microsoft.EntityFrameworkCore;
using StockWebApp1.Models;

namespace StockWebApp1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
