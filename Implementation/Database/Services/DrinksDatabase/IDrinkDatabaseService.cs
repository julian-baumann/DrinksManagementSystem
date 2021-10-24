using Database.Models;

namespace Database.Services.DrinksDatabase
{
    public interface IDrinkDatabaseService : IDatabaseService<DrinkModel, int>
    {
    }
}