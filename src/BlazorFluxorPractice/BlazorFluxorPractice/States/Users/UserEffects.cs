using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BlazorFluxorPractice.Models;
using Fluxor;

namespace BlazorFluxorPractice.States.Users
{
    public class UsersEffects
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UsersEffects> _logger;
        private readonly IDispatcher _dispatcher;

        public UsersEffects(HttpClient httpClient, ILogger<UsersEffects> logger, IDispatcher dispatcher)
        {
            _httpClient = httpClient;
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [EffectMethod]
        public async Task HandleFetchDataEffect(FetchUsersAction action, IDispatcher dispatcher)
        {
            try
            {
                _dispatcher.Dispatch(new LoadingUsersAction());
                
                var users = await _httpClient.GetFromJsonAsync<User[]>("User/GetUsers");
                _dispatcher.Dispatch(new LoadUsersAction { Users = users.ToList() });
            }
            catch (Exception e)
            {
                _logger.LogError("Error fetching users", e);
                _dispatcher.Dispatch(new LoadUsersFailedAction());
            }
        }

        [EffectMethod]
        public async Task HandleDeleteUserEffect(DeleteUserAction action, IDispatcher dispatcher)
        {
            try
            {
                await _httpClient.DeleteAsync($"User/GetSingleUser/{action.userId}");
                
                _dispatcher.Dispatch(new DeleteUserActionSuccess(action.userId));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error deleting user with ID {action.userId}", e);
                _dispatcher.Dispatch(new DeleteUserActionFailure());
            }
        }

        [EffectMethod]
        public async Task HandleCreateUserEffect(CreateUserAction action , IDispatcher dispatcher)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(action.User); 
                
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
        
                var response = await _httpClient.PostAsync($"/User/AddUser", httpContent); 

                if(response.IsSuccessStatusCode)
                {
                    var userId = int.Parse(await response.Content.ReadAsStringAsync());
                    
                    action.User.UserId = userId;

                    _dispatcher.Dispatch(new CreateUserActionSuccess(action.User));
                }
                else
                {
                    throw new Exception("Failed to add user");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error creating user", e);
                _dispatcher.Dispatch(new CrateUserActionFailure());
            }
        }

        [EffectMethod]
        public async Task HandleUpdateUser(UpdateUserAction action, IDispatcher dispatcher)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(action.User); 

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/User/EditUser", httpContent); 
        
                if(response.IsSuccessStatusCode)
                {
                    dispatcher.Dispatch(new UpdateUserActionSuccess(action.User));
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();

                    throw new Exception($"Failed to update user. Server returned: {errorContent}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                dispatcher.Dispatch(new UpdateUserActionFailure());
            }
        }
    }
};