using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Services.Database
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        private string _databasePath;

        public DbSet<UserDto> Users { get; set; }
        public DbSet<DrinkDto> Drinks { get; set; }
        public DbSet<BoughtDrinkDto> BoughtDrinks { get; set; }
        public DbSet<DrinkBrandDto> Brands { get; set; }

        public DatabaseService()
        {
            SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();
        }

        public void SetDatabasePath(string path)
        {
            _databasePath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Filename={_databasePath}");
        }
    }
}