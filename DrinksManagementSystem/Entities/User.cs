using System;
using System.IO;
using Database.Models;
using DrinksManagementSystem.Core;
using DrinksManagementSystem.Helpers;

namespace DrinksManagementSystem.Entities
{
    public class User : Notifiable
    {
        public int Id { get; set; }

        private string _name;
        private string _imagePath;
        private UserRoles _role = UserRoles.Guest;


        public string Name { get => _name; set => Set(ref _name, value); }
        public string ImagePath { get => _imagePath; set => Set(ref _imagePath, value); }
        public UserRoles Role { get => _role; set => Set(ref _role, value); }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string FullImagePath
        {
            get
            {
                if (AppCore.StoragePath != null && ImagePath != null)
                {
                    return Path.Combine(AppCore.StoragePath, ImagePath);
                }

                return null;
            }
        }

        public User() { }

        public User(UserModel model)
        {
            FromDto(model);
        }

        public void FromDto(UserModel model)
        {
            Id = model.Id;
            Name = model.Name;
            ImagePath = model.ImagePath;
            DateCreated = model.DateCreated;
            DateModified = model.DateModified;
            Role = model.Role.ToEnum<UserRoles>(UserRoles.Guest);
        }

        public UserModel ToDto()
        {
            var dto = new UserModel
            {
                Id = Id,
                Name = Name,
                ImagePath = ImagePath,
                Role = Role.ToLowerCamelCaseString(),
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            return dto;
        }

        public User Clone()
        {
            return new User()
            {
                Id = Id,
                Name = Name,
                ImagePath = ImagePath,
                Role = Role,
                DateCreated = DateCreated,
                DateModified = DateModified
            };
        }
    }
}
