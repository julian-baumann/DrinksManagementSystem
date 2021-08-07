using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        private static string _databasePath;

        public DbSet<UserDto> Users { get; set; }
        public DbSet<DrinkDto> Drinks { get; set; }
        public DbSet<BoughtDrinkDto> BoughtDrinks { get; set; }
        public DbSet<DrinkBrandDto> Brands { get; set; }

        public DatabaseContext()
        {
            SQLitePCL.Batteries_V2.Init();
            // Database.OpenConnection();
            Database.EnsureCreated();
            Database.Migrate();
        }

        public static void SetDatabasePath(string path)
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