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
            IUserDatabaseService userDatabaseService,
            IStorageService storageService
            )
        {
            _userDatabaseService = userDatabaseService;
            _storageService = storageService;
        }

        public void Start()
        {
            _userDatabaseService.Start();
        }

        public async Task<ObservableCollection<Entities.User>> GetAll()
        {
            var users = await _userDatabaseService.GetUsers();

            foreach (var userDto in users)
            {
                Users.Add(new Entities.User(userDto));
            }

            return Users;
        }

        public async Task<Entities.User> Get(int id)
        {
            if (Users?.Count > 0)
            {
                return Users.FirstOrDefault(drink => drink.Id == id);
            }

            var userDto = await _userDatabaseService.GetUser(id);
            return new Entities.User(userDto);
        }

        public async Task<bool> Create(Entities.User user)
        {
            try
            {
                var newId = await _userDatabaseService.CreateUser(user.ToDto());

                if (newId == null) return false;

                user.Id = (int)newId;
                Users.Add(user);

                return true;
            }
            catch(Exception exception)
            {
                Logger.Exception(exception);
            }

            return false;
        }


        public async Task<int> Update(Entities.User user)
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

        public async Task<int> Remove(Entities.User user)
        {
            if (user.ImagePath != null)
            {
                await _storageService.RemovePicture(user.ImagePath);
            }

            var result = await _userDatabaseService.RemoveUser(user.Id);

            if (result < 0) return result;

            var index = Users.IndexOf(Users.First(u => u.Id == user.Id));

            if (index >= 0)
            {
                Users.RemoveAt(index);
            }

            return result;
        }
    }
}