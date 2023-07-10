using Microsoft.EntityFrameworkCore;

namespace WebApplicationCenkY.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Feed> Feeds { get; set; }


        

    }
}
