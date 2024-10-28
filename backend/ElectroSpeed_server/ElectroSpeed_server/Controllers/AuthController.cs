using ElectroSpeed_server.Controllers;
using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ElectroSpeedContext _esContext;
        private readonly PasswordHasher<Usuarios> _passwordHasher;
        private readonly TokenValidationParameters _tokenValidationParameter;
        private readonly UserController _userController;

        public AuthController(ElectroSpeedContext esContext, UserController userController, IOptionsMonitor<JwtBearerOptions> jwOptions)
        {
            _esContext = esContext;
            _passwordHasher = new PasswordHasher<Usuarios>();
            _userController = userController;
            _tokenValidationParameter = jwOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
        }
        [HttpPost("/register")]
        public ActionResult Register([FromBody] RegisterRequest model)
        {
            if (_esContext.Usuarios.Any(usuario => usuario.Username == model.Username))
            {
                return BadRequest("El nombre de usuario ya está en uso");
            }

            Usuarios newUser = new Usuarios
            {
                Username = model.Username,
                Password = _passwordHasher.HashPassword(null, model.Password)
            };

            _esContext.Usuarios.Add(newUser);
            _esContext.SaveChangesAsync();

            return Ok("Usuario registrado");

        }

        [HttpPost("/login")]
        public ActionResult<Usuarios> Login([FromBody] LoginRequest model)
        {
            Usuarios[] usuarios = _userController.GetUsuarios().ToArray();
            foreach (var user in usuarios)
            {
                if (user.Username == model.Username && user.Password == model.Password)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Claims = new Dictionary<string, object>
                        {
                            {ClaimTypes.Name, model.Username}
                        },
                        Expires = DateTime.UtcNow.AddYears(3),
                        SigningCredentials = new SigningCredentials(
                             _tokenValidationParameter.IssuerSigningKey,
                             SecurityAlgorithms.HmacSha256Signature)
                    };

                    JwtSecurityTokenHandler tokenHadler = new JwtSecurityTokenHandler();
                    SecurityToken token = tokenHadler.CreateToken(tokenDescriptor);
                    string stringToken = tokenHadler.WriteToken(token);

                    return Ok(stringToken);
                }
            }


            return Unauthorized("Email o contraseña incorrecto");
        }
        
    }

}