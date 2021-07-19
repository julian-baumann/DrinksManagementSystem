using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Services.BoughtDrinkDatabase;

namespace DrinksManagementSystem.Services.BoughtDrink
{
    public class BoughtDrinkService : IBoughtDrinkService
    {
        private readonly IBoughtDrinkDatabaseService _databaseService;

        public BoughtDrinkService(IBoughtDrinkDatabaseService boughtDrinkDatabaseService)
        {
            _databaseService = boughtDrinkDatabaseService;
        }

        public void Start()
        {
            _databaseService.Start();
        }

        public async Task<Entities.BoughtDrink[]> GetAll()
        {
            var users = await _databaseService.GetAll();

            return users.Select(dto => new Entities.BoughtDrink(dto)).ToArray();
        }

        public async Task<Entities.BoughtDrink[]> GetAllByUser(int userId)
        {
            var users = await _databaseService.GetAllByUser(userId);

            return users.Select(dto => new Entities.BoughtDrink(dto)).ToArray();
        }

        public async Task<Entities.BoughtDrink> Get(int id)
        {
            var userDto = await _databaseService.Get(id);
            return new Entities.BoughtDrink(userDto);
        }

        public async Task<bool> Create(Entities.BoughtDrink boughtDrink)
        {
            try
            {
                var newId = await _databaseService.Create(boughtDrink.ToDto());

                if (newId == null) return false;

                boughtDrink.Id = (int)newId;

                return true;
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }

            return false;
        }

        public async Task<int> Update(Entities.BoughtDrink boughtDrink)
        {
            try
            {
                var result = await _databaseService.Update(boughtDrink.ToDto());

                if (result < 0) return result;

                return result;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

            return -1;
        }

        public Task<int> Remove(int id)
        {
            return _databaseService.Remove(id);
        }
    }
}