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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SauceType>().HasKey(sh => new { sh.SauceId, sh.TypeId });

            modelBuilder.Entity<SauceType>()
                .HasOne(sh => sh.Sauce)
                .WithMany(s => s.SauceTypes)
                .HasForeignKey(s => s.SauceId);

            modelBuilder.Entity<SauceType>()
                .HasOne(sh => sh.Type)
                .WithMany(s => s.SauceTypes)
                .HasForeignKey(s => s.TypeId);

            modelBuilder.Entity<Sauce>()
                .HasOne(s => s.Author)
                .WithMany(a => a.Sauces)
                .HasForeignKey(s => s.AuthorId);
            modelBuilder.Entity<SauceHistory>()
                .HasOne(sh => sh.Sauce)
                .WithMany(s => s.SauceHistory)
                .HasForeignKey(s => s.SauceId);
        }
    }
}

