using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Entities;

namespace Database.Services.UserDatabase
{
    public class UserDatabaseService : IUserDatabaseService
    {
        public List<UserDto> GetUsers()
        {
            try
            {
                using var database = new DatabaseContext();
                return database.Users.ToList();
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public UserDto GetUser(int id)
        {
            try
            {
                using var database = new DatabaseContext();
                return database.Users.Single(entry => entry.Id == id);
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<int?> CreateUser(UserDto userDto)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.Users.Add(userDto);
                await database.SaveChangesAsync();

                return result?.Entity?.Id;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {

            try
            {
                await using var database = new DatabaseContext();
                database.Users.Update(userDto);
                var result = await database.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return false;
            }
        }

        public async Task<bool> RemoveUser(int id)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Users.Remove(GetUser(id));
                var result = await database.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return false;
            }
        }
    }
}