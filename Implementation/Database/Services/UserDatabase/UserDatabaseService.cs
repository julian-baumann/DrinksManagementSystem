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
        public List<UserModel> GetAll()
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

        public UserModel Get(int id)
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

        public DateTime? GetLatestChange(int userId)
        {
            try
            {
                using var database = new DatabaseContext();
                var user = database.Users.OrderByDescending(user => user.DateModified).FirstOrDefault();

                return user?.DateModified;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return null;
            }
        }

        public async Task<int> Create(UserModel userModel)
        {
            try
            {
                await using var database = new DatabaseContext();
                var result = database.Users.Add(userModel);
                await database.SaveChangesAsync();

                if (result?.Entity?.Id != null)
                {
                    return result.Entity.Id;
                }

                return -1;

            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
                return -1;
            }
        }

        public async Task<bool> Update(UserModel userModel)
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

        public async Task<bool> Remove(int id)
        {
            try
            {
                await using var database = new DatabaseContext();
                database.Users.Remove(Get(id));
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