using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Services.BoughtDrinkDatabase;

namespace DrinksManagementSystem.Services.BoughtDrink
{
    public class BoughtDrinkService : IBoughtDrinkService
    {
        private readonly IBoughtDrinkDatabaseService _databaseService;

        public BoughtDrinkService(
            IBoughtDrinkDatabaseService boughtDrinkDatabaseService
        )
        {
            _databaseService = boughtDrinkDatabaseService;
        }

        public Entities.BoughtDrink[] GetAll()
        {
            var users = _databaseService.GetAll();
            return users.Select(dto => new Entities.BoughtDrink(dto)).ToArray();
        }

        public Entities.BoughtDrink[] GetAll(DateTime fromDate, DateTime toDate, bool onlyFlat)
        {
            var users = _databaseService.GetAll(fromDate, toDate, onlyFlat);
            return users.Select(dto => new Entities.BoughtDrink(dto)).ToArray();
        }

        public Entities.BoughtDrink[] GetAllUnpaidDrinksByUser(int userId)
        {
            var users = _databaseService.GetAllUnpaidByUser(userId);
            return users.Select(dto => new Entities.BoughtDrink(dto)).Where(dto => dto.Flat is false).ToArray();
        }

        public Entities.BoughtDrink[] GetAllPaidDrinks()
        {
            var users = _databaseService.GetAllPaid();
            return users.Select(dto => new Entities.BoughtDrink(dto)).Where(dto => dto.Flat is false).ToArray();
        }

        public Entities.BoughtDrink[] GetAllPaidDrinksByUser(int userId)
        {
            var users = _databaseService.GetAllPaidByUser(userId);
            return users.Select(dto => new Entities.BoughtDrink(dto)).ToArray();
        }

        public Entities.BoughtDrink Get(int id)
        {
            var userDto = _databaseService.Get(id);
            return new Entities.BoughtDrink(userDto);
        }

        public async Task<bool> Create(Entities.BoughtDrink boughtDrink)
        {
            try
            {
                var newId = await _databaseService.Create(boughtDrink.ToDto());

                if (newId == -1) return false;

                boughtDrink.Id = (int) newId;

                return true;
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }

            return false;
        }

        public async Task<bool> Update(Entities.BoughtDrink boughtDrink)
        {
            try
            {
                var result = await _databaseService.Update(boughtDrink.ToDto());
                return result;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

            return false;
        }

        public Task<bool> Remove(int id)
        {
            return _databaseService.Remove(id);
        }
    }
}