using System.Net.Http;
using System.Threading.Tasks;
using DrinksManagementSystem.Services.BoughtDrink;
using DrinksManagementSystem.Services.User;

namespace DrinksManagementSystem.Services.Backend
{
    public class BackendService : IBackendService
    {
        private readonly IUserService _userService;
        private readonly IBoughtDrinkService _boughtDrinkService;

        private string _serverUrl;

        public BackendService(
            IUserService userService,
            IBoughtDrinkService boughtDrinkService
        )
        {
            _userService = userService;
            _boughtDrinkService = boughtDrinkService;
        }

        public void Initialize(string serverUrl)
        {
            _serverUrl = serverUrl;
        }

        public async Task UploadUsers()
        {
            using var httpClient = new HttpClient();

            httpClient.GetAsync($"{_serverUrl}/api/v1/users/last");
        }

        public Task UploadBoughtDrinks()
        {
            throw new System.NotImplementedException();
        }
    }
}