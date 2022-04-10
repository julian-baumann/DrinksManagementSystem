using Common.Core;
using Common.Entities;
using Database.Models;
using Database.Services.UserDatabase;
using Microsoft.AspNetCore.Mvc;

namespace BackendServer.RestApi.V1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDatabaseService _userDatabaseService;

        public UsersController()
        {
            _userDatabaseService = Ioc.Resolve<IUserDatabaseService>();
        }

        [HttpPost]
        [Route("users")]
        public ActionResult UpdateUsers(ChangelogItem<UserModel>[] usersChanges)
        {
            foreach (var change in usersChanges)
            {
                if (change.Type == ChangeTypes.Added)
                {
                    foreach (var model in change.Item)
                    {
                        _userDatabaseService.Create(model);
                    }
                }
                else if (change.Type == ChangeTypes.Modified)
                {
                    foreach (var model in change.Item)
                    {
                        _userDatabaseService.Update(model);
                    }
                }
                else if (change.Type == ChangeTypes.Deleted)
                {
                    foreach (var model in change.Item)
                    {
                        _userDatabaseService.Remove(model.Id);
                    }
                }
            }

            return Ok();
        }
    }
}