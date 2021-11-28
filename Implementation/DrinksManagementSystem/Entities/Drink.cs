using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Database.Models;
using DrinksManagementSystem.Core;

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
        public double? FlatPrice { get; set; }
        public int? Quantity { get; set; }
        public List<string> BrandIds { get; set; } = new ();
        public ObservableCollection<DrinkBrand> Brands { get; set; } = new ();
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

        public Drink(DrinkModel model)
        {
            FromDto(model);
        }

        public void FromDto(DrinkModel model)
        {
            Id = model.Id;
            Name = model.Name;
            ImagePath = model.ImagePath;
            AlcoholContent = model.AlcoholContent;
            Price = model.Price;
            AdminPrice = model.AdminPrice;
            FlatPrice = model.FlatPrice;
            Quantity = model.Quantity;
            BrandIds = model.BrandIds?.Split(';').ToList();
            Type = model.Type;
            DateCreated = model.DateCreated;
            DateModified = model.DateModified;
        }

        public DrinkModel ToDto()
        {
            var brandIds = Brands.Select(brand => brand.Id).ToList();

            var dto = new DrinkModel()
            {
                Id = Id,
                Name = Name,
                ImagePath = ImagePath,
                AlcoholContent = AlcoholContent,
                Price = Price,
                AdminPrice = AdminPrice,
                FlatPrice = FlatPrice,
                Quantity = Quantity,
                BrandIds = string.Join(";", brandIds),
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
                FlatPrice = FlatPrice,
                Quantity = Quantity,
                BrandIds = BrandIds != null ? new List<string>(BrandIds) : new List<string>(),
                Type = Type,
                DateCreated = DateCreated,
                DateModified = DateModified
            };

            return dto;
        }
    }
}