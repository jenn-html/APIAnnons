using APIAnnons.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAnnons.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Ad> Ads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>().Property(a => a.Price).HasPrecision(18, 2);
        }
    }
}
