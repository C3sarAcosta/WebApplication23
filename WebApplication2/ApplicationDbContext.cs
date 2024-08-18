using Microsoft.EntityFrameworkCore;

namespace WebApplication2
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
            public DbSet<MyEntity> MyEntities { get; set; }
    }
}
