using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Database.Services.UserDatabase;
using DrinksManagementSystem.Services.Storage;

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

        public ObservableCollection<Entities.User> GetAll()
        {
            var users = _userDatabaseService.GetAll();

            if (users == null) return null;

            foreach (var userDto in users)
            {
                Users.Add(new Entities.User(userDto));
            }

            return Users;
        }

        public Entities.User Get(int id)
        {
            if (Users?.Count > 0)
            {
                return Users.FirstOrDefault(drink => drink.Id == id);
            }

            var userDto = _userDatabaseService.Get(id);
            return new Entities.User(userDto);
        }

        public async Task<bool> Create(Entities.User user)
        {
            try
            {
                var newId = await _userDatabaseService.Create(user.ToDto());

                if (newId == -1) return false;

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


        public async Task<bool> Update(Entities.User user)
        {
            try
            {
                var result = await _userDatabaseService.Update(user.ToDto());

                if (!result) return false;

                var userFromList = Users.FirstOrDefault(u => u.Id == user.Id);
                var index = Users.IndexOf(userFromList);

                if (index >= 0)
                {
                    Users[index] = user;
                }

                return true;
            }
            catch (Exception exception)
            {
                Logger.Exception(exception);
            }

            return true;
        }

        public async Task<bool> Remove(Entities.User user)
        {
            if (user.ImagePath != null)
            {
                await _storageService.RemovePicture(user.ImagePath);
            }

            var result = await _userDatabaseService.Remove(user.Id);

            if (!result) return false;

            var index = Users.IndexOf(Users.First(u => u.Id == user.Id));

            if (index >= 0)
            {
                Users.RemoveAt(index);
            }

            return true;
        }
    }
}