using System;
using System.IO;
using Database.Entities;
using DrinksManagementSystem.Core;

// ReSharper disable MemberCanBePrivate.Global

namespace DrinksManagementSystem.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double? AlcoholContent { get; set; }
        public double? Price { get; set; }
        public double? AdminPrice { get; set; }
        public int? Quantity { get; set; }
        public string? BrandId { get; set; }
        public DrinkBrand? Brand { get; set; }
        public string Type { get; set; }
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

        public Drink() { }

        public Drink(Database.Entities.Drink dto)
        {
            FromDto(dto);
        }

        public void FromDto(Database.Entities.Drink dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            ImagePath = dto.ImagePath;
            AlcoholContent = dto.AlcoholContent;
            Price = dto.Price;
            AdminPrice = dto.AdminPrice;
            Quantity = dto.Quantity;
            BrandId = dto.BrandId;
            Type = dto.Type;
            DateCreated = dto.DateCreated;
            DateModified = dto.DateModified;
        }

        public Database.Entities.Drink ToDto()
        {
            var dto = new Database.Entities.Drink()
            {
                Id = Id,
                Name = Name,
                ImagePath = ImagePath,
                AlcoholContent = AlcoholContent,
                Price = Price,
                AdminPrice = AdminPrice,
                Quantity = Quantity,
                BrandId = Brand?.Id,
                Type = Type,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            return dto;
        }

        public Drink Clone()
        {
            var dto = new Drink()
            {
                Id = Id,
                Name = Name,
                ImagePath = ImagePath,
                AlcoholContent = AlcoholContent,
                Price = Price,
                AdminPrice = AdminPrice,
                Quantity = Quantity,
                BrandId = BrandId,
                Brand = Brand?.Clone(),
                Type = Type,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            return dto;
        }
    }
}