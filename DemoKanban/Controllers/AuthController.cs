using DemoKanban.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Reinforced.Typings.Attributes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace WebAPI.Auth.Controllers
{
    [TsInterface(AutoI = true)]
    public class LoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    [TsInterface(AutoI = true)]
    public record AccecTokenDto(string Value);

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthController(ILogger<AuthController> logger, 
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto dto)
        {
            var user = await signInManager.UserManager.FindByNameAsync(dto.Login);

            if (user == null) return Unauthorized();

            var signInResult = 
                await signInManager.PasswordSignInAsync(user, dto.Password, true, true);


            if (!signInResult.Succeeded) return Unauthorized();

            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", "1"),
                    new Claim(ClaimTypes.Email, "daniel@test.pl"),
                    new Claim(JwtRegisteredClaimNames.Sub, "Daniel")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptior);

            return Ok(new AccecTokenDto(tokenHandler.WriteToken(token)));
        }
    }
}
