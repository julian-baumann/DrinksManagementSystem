using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Services.Database
{
    public interface IDatabaseService
    {
        DbSet<UserDto> Users { get; set; }
        DbSet<DrinkDto> Drinks { get; set; }
        DbSet<BoughtDrinkDto> BoughtDrinks { get; set; }
        DbSet<DrinkBrandDto> Brands { get; set; }

        void SetDatabasePath(string path);
    }
}