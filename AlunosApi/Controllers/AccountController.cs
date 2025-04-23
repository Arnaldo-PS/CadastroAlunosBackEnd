using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlunosApi.Services;
using AlunosApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authentication;

        public AccountController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(authentication));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("As senhas não conferem");
            }
            var result = await _authentication.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Ok(new { message = "Usuário criado com sucesso!" });
            }
            else
            {
                return BadRequest("Erro ao criar usuário");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel model)
        {
            var result = await _authentication.Authenticate(model.Email, model.Password);
            if (result)
            {
                return GenerateToken(model);
            }
            else
            {
                return Unauthorized("Usuário ou senha inválidos");
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("meuToken", "token do usuario"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
