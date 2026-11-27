using APIAnnons.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAnnons.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void MigrateData()
        {
            dbContext.Database.Migrate();
            SeedData();
            dbContext.SaveChanges();
        }

        public void SeedData()
        {
            if (!dbContext.Ads.Any(a => a.Title == "Cykel"))
            {
                dbContext.Add(new Ad
                {
                    Title = "Cykel",
                    Description = "Blå damcykel, 3 växlar",
                    Price = 1200,
                    Category = "Fordon"
                });
            }
        }
    }
}
