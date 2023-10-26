using hcode.Entity;
using Microsoft.EntityFrameworkCore;

namespace hcode.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Sauce> Sauces { get; set; }
        public DbSet<SauceHistory> SauceHistories { get; set; }
        public DbSet<SauceType> SauceTypes { get; set; }
        public DbSet<Types> Types { get; set; }
    }
}

