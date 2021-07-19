using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Database.Entities;
using DrinksManagementSystem.Core;

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

        public User(Database.Entities.User dto)
        {
            FromDto(dto);
        }

        public void FromDto(Database.Entities.User dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            ImagePath = dto.ImagePath;
            DateCreated = dto.DateCreated;
            DateModified = dto.DateModified;

            var role = UserRoles.Guest;
            Enum.TryParse(dto.Role, true, out role);
            Role = role;

        }

        public Database.Entities.User ToDto()
        {
            var dto = new Database.Entities.User
            {
                Id = Id,
                Name = Name,
                ImagePath = ImagePath,
                Role = Role.ToString(),
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
