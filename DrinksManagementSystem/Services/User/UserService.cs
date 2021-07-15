using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Common.Core;
using Database.Services;
using DrinksManagementSystem.Services.Storage;
using DrinksManagementSystem.Entities;
using System.Linq;

namespace DrinksManagementSystem.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserDatabaseService _userDatabaseService;
        private readonly IStorageService _storageService;

        public ObservableCollection<Entities.User> Users { get; set; } = new ObservableCollection<Entities.User>();

        public UserService(
            IUserDatabaseService userUserDatabaseService,
            IStorageService storageService
            )
        {
            _userDatabaseService = userUserDatabaseService;
            _storageService = storageService;
        }

        public void Connect(string path)
        {
            _userDatabaseService.Connect(path);
        }

        public async Task<ObservableCollection<Entities.User>> GetUsers()
        {
            var users = await _userDatabaseService.GetUsers();

            foreach (var userDto in users)
            {
                Users.Add(new Entities.User(userDto));
            }

            return Users;
        }

        public async Task<Entities.User> GetUser(int id)
        {
            var userDto = await _userDatabaseService.GetUser(id);
            return new Entities.User(userDto);
        }

        public async Task<int> CreateUser(Entities.User user)
        {
            try
            {
                var result = await _userDatabaseService.CreateUser(user.ToDto());

                if (result >= 0)
                {
                    Users.Add(user);
                }

                return result;
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }

            return -1;
        }


        public async Task<int> UpdateUser(Entities.User user)
        {
            try
            {
                var result = await _userDatabaseService.UpdateUser(user.ToDto());

                if (result < 0) return result;

                var userFromList = Users.FirstOrDefault(u => u.Id == user.Id);
                var index = Users.IndexOf(userFromList);

                if (index >= 0)
                {
                    Users[index] = user;
                }

                return result;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

            return -1;
        }

        public async Task<int> RemoveUser(Entities.User user)
        {
            await _storageService.RemoveProfilePicture(user.ImagePath);

            var result = await _userDatabaseService.RemoveUser(user.Id);

            if (result < 0) return result;

            var index = Users.IndexOf(Users.FirstOrDefault(u => u.Id == user.Id));
            Users.RemoveAt(index);

            return result;
        }
    }
}