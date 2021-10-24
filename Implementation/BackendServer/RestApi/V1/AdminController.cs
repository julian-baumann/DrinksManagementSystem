using System;
using System.Collections.Generic;
using Common.Core;
using Common.Entities;
using Database.Models;
using Database.Services.BoughtDrinkDatabase;
using Database.Services.DrinkBrandDatabase;
using Database.Services.DrinksDatabase;
using Database.Services.UserDatabase;
using Microsoft.AspNetCore.Mvc;

namespace BackendServer.RestApi.V1
{
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserDatabaseService _userDatabaseService;
        private readonly IDrinkDatabaseService _drinkDatabaseService;
        private readonly IBoughtDrinkDatabaseService _boughtDrinkDatabaseService;
        private readonly IDrinkBrandDatabaseService _drinkBrandDatabaseService;

        public AdminController()
        {
            _userDatabaseService = Ioc.Resolve<IUserDatabaseService>();
            _drinkDatabaseService = Ioc.Resolve<IDrinkDatabaseService>();
            _boughtDrinkDatabaseService = Ioc.Resolve<IBoughtDrinkDatabaseService>();
            _drinkBrandDatabaseService = Ioc.Resolve<IDrinkBrandDatabaseService>();
        }

        private static void UpdateDatabaseEntries<T>(IEnumerable<ChangelogItem<T>> changes, Action<T> createFunction, Action<T> updateFunction, Action<T> removeFunction)
        {
            foreach (var change in changes)
            {
                if (change.Type == ChangeTypes.Added)
                {
                    foreach (var model in change.Item)
                    {
                        createFunction(model);
                    }
                }
                else if (change.Type == ChangeTypes.Modified)
                {
                    foreach (var model in change.Item)
                    {
                        updateFunction(model);
                    }
                }
                else if (change.Type == ChangeTypes.Deleted)
                {
                    foreach (var model in change.Item)
                    {
                        removeFunction(model);
                    }
                }
            }
        }

        [HttpPost]
        [Route("users")]
        public ActionResult UpdateUsers(ChangelogItem<UserModel>[] usersChanges)
        {
            UpdateDatabaseEntries(usersChanges,
                model => _userDatabaseService.Create(model),
                model => _userDatabaseService.Create(model),
                model => _userDatabaseService.Remove(model.Id)
            );

            return Ok();
        }


        [HttpPost]
        [Route("drinks")]
        public ActionResult UpdateDrinks(ChangelogItem<DrinkModel>[] drinkChanges)
        {
            UpdateDatabaseEntries(drinkChanges,
                model => _drinkDatabaseService.Create(model),
                model => _drinkDatabaseService.Create(model),
                model => _drinkDatabaseService.Remove(model.Id)
            );

            return Ok();
        }

        [HttpPost]
        [Route("drink-brands")]
        public ActionResult UpdateDrinkBrands(ChangelogItem<DrinkBrandModel>[] drinkChanges)
        {
            UpdateDatabaseEntries(drinkChanges,
                model => _drinkBrandDatabaseService.Create(model),
                model => _drinkBrandDatabaseService.Create(model),
                model => _drinkBrandDatabaseService.Remove(model.Id)
            );

            return Ok();
        }

        [HttpPost]
        [Route("bought-drinks")]
        public ActionResult UpdateBoughtDrinks(ChangelogItem<BoughtDrinkModel>[] drinkChanges)
        {
            UpdateDatabaseEntries(drinkChanges,
                model => _boughtDrinkDatabaseService.Create(model),
                model => _boughtDrinkDatabaseService.Create(model),
                model => _boughtDrinkDatabaseService.Remove(model.Id)
            );

            return Ok();
        }
    }
}
