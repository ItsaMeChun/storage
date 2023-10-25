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

        public DbSet<User> user { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Sauce> Sauce { get; set; }
        public DbSet<SauceHistory> SauceHistory { get; set; }
        public DbSet<SauceType> SauceType { get; set; }
        public DbSet<Types> Type { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

