using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
        public string[] BrandIds { get; set; }
        public ObservableCollection<DrinkBrand> Brands { get; set; } = new();
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

        public Drink(Database.Entities.DrinkDto dto)
        {
            FromDto(dto);
        }

        public void FromDto(Database.Entities.DrinkDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            ImagePath = dto.ImagePath;
            AlcoholContent = dto.AlcoholContent;
            Price = dto.Price;
            AdminPrice = dto.AdminPrice;
            Quantity = dto.Quantity;
            BrandIds = dto.BrandIds;
            Type = dto.Type;
            DateCreated = dto.DateCreated;
            DateModified = dto.DateModified;
        }

        public Database.Entities.DrinkDto ToDto()
        {
            var brandIds = Brands.Select(brand => brand.Id).ToList();

            var dto = new Database.Entities.DrinkDto()
            {
                Id = Id,
                Name = Name,
                ImagePath = ImagePath,
                AlcoholContent = AlcoholContent,
                Price = Price,
                AdminPrice = AdminPrice,
                Quantity = Quantity,
                BrandIds = brandIds.ToArray(),
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
                BrandIds = BrandIds,
                Type = Type,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            if (Brands?.Count > 0)
            {
                foreach (var brand in Brands)
                {
                    Brands.Add(brand.Clone());
                }
            }

            return dto;
        }
    }
}