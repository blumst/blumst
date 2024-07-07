using Microsoft.EntityFrameworkCore;

namespace StockWebApp1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }
    }
}
