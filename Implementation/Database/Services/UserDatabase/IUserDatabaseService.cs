using System;
using Database.Models;

namespace Database.Services.UserDatabase
{
    public interface IUserDatabaseService : IDatabaseService<UserModel, int>
    {
        DateTime? GetLatestChange(int id);
    }
}