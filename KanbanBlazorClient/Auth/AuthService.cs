using KanbanBlazorClient.Dtos;
using KanbanDemo.Dtos;
using System.Net.Http.Json;

namespace KanbanBlazorClient.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;

        public AuthService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<LoginComponentResultDto> Login(LoginComponentDto login)
        {
            using var msg = await client.PostAsJsonAsync<LoginDto>("api/auth", new LoginDto() { Login = login.Login, Password = login.Password });
            
            if (msg.IsSuccessStatusCode)
            {
                var tokenDto = await msg.Content.ReadFromJsonAsync<AccessTokenDto>();
                return new LoginComponentResultDto(LoginStatus.Success, "", tokenDto.Value, tokenDto.Expires);
            }

            return new LoginComponentResultDto(LoginStatus.Error, "Login failed, try again.", "", DateTime.Now);
        }
    }
}
