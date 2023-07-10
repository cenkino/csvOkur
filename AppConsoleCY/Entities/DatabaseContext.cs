using Microsoft.EntityFrameworkCore;

namespace AppConsoleCY.Entities
{
    public class DatabaseContext : DbContext
    {
        

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=WebAppCY;Trusted_Connection=true");
        }


    }
}
