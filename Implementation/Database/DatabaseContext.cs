using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public sealed class DatabaseContext : DbContext
    {
        private static string _databasePath;

        public DbSet<UserModel> Users { get; set; }
        public DbSet<DrinkModel> Drinks { get; set; }
        public DbSet<BoughtDrinkModel> BoughtDrinks { get; set; }
        public DbSet<DrinkBrandModel> Brands { get; set; }

        public DatabaseContext()
        {
            SQLitePCL.Batteries_V2.Init();
            // Database.OpenConnection();
            // Database.EnsureCreated();
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