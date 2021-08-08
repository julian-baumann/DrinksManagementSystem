using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Models;

namespace Database.Services.UserDatabase
{
    public class UserDatabaseService : IUserDatabaseService
    {
        public List<UserModel> GetUsers()
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

        public UserModel GetUser(int id)
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

        public async Task<int?> CreateUser(UserModel userModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.Users.Add(userModel);
                await database.SaveChangesAsync();

                return result?.Entity?.Id;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<bool> UpdateUser(UserModel userModel)
        {

            try
            {
                await using var database = new DatabaseContext();
                database.Users.Update(userModel);
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