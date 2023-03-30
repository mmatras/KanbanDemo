using KanbanDemo.Dtos;
using Microsoft.JSInterop;

namespace KanbanBlazorClient.Auth
{
    public record AccessTokenServiceDto(string Token, DateTime expires);

    public class TokenService : ITokenService
    {
        private readonly IJSRuntime jsr;

        public TokenService(IJSRuntime jsr)
        {
            this.jsr = jsr;
        }

        public async Task<AccessTokenServiceDto?> GetToken()
        {
            var token = await jsr.InvokeAsync<string>("localStorage.getItem", "accessToken");
            var expires = await jsr.InvokeAsync<string>("localStorage.getItem", "accessTokenExp");
            return string.IsNullOrEmpty(token) || string.IsNullOrEmpty(expires) ? null : 
                new AccessTokenServiceDto(token, DateTime.Parse(expires));
        }

        public async Task RemoveToken()
        {
            await jsr.InvokeVoidAsync("localStorage.removeItem", "accessToken");
            await jsr.InvokeVoidAsync("localStorage.removeItem", "accessTokenExp");
        }

        public async Task SetToken(string token, DateTime expires)
        {
            await jsr.InvokeVoidAsync("localStorage.setItem", "accessToken", token);
            await jsr.InvokeVoidAsync("localStorage.setItem", "accessTokenExp", $"{expires}");
        }
    }
}
