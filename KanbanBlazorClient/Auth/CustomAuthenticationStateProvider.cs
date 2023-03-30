using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace KanbanBlazorClient.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenService tokenService;
        private readonly ILogger<CustomAuthenticationStateProvider> logger;

        public CustomAuthenticationStateProvider(ITokenService tokenService, ILogger<CustomAuthenticationStateProvider> logger)
        {
            this.tokenService = tokenService;
            this.logger = logger;
        }

        public void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenDto = await tokenService.GetToken();
            ClaimsIdentity claimsIdentity;
            if(string.IsNullOrEmpty(tokenDto?.Token) || tokenDto.expires < DateTime.Now)
            {
                claimsIdentity = new ClaimsIdentity();
            } 
            else
            {
                var claims = ParseClaimsFromJwt(tokenDto.Token);
                claimsIdentity = new ClaimsIdentity(claims, "jwt");
            }

            return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var claimsString = token.Split('.')[1];
            var claimsBytes = Convert.FromBase64String(claimsString);
            var claimsDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(claimsBytes);

            //List<Claim> claims = new List<Claim>();
            //foreach (var claim in claimsDictionary)
            //{
            //    var key = claim.Key;
            //    var value = claim.Value;

            //    logger.LogDebug($"key: {key} value: {value}");

            //    claims.Add(new Claim(key, value?.ToString() ?? ""));
            //}


            var claims = claimsDictionary.Select(pair => new Claim(pair.Key, pair.Value?.ToString() ?? "")).ToList();
            return claims;
        }
    }
}
