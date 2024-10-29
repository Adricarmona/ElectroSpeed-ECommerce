using ElectroSpeed_server.Controllers;
using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
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
        private readonly PasswordHelper _passwordHelper;
        private readonly ElectroSpeedContext _esContext;
        private readonly TokenValidationParameters _tokenValidationParameter;
        private readonly UserController _userController;

        public AuthController(ElectroSpeedContext esContext, UserController userController, IOptionsMonitor<JwtBearerOptions> jwOptions)
        {
            _passwordHelper = new PasswordHelper();
            _esContext = esContext;
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
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                Password = PasswordHelper.Hash(model.Password)
            };

            _esContext.Usuarios.Add(newUser);
            _esContext.SaveChanges();

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

                                return Ok(new
                    {
                        accessToken = stringToken
                    });

        }

        [HttpPost("/login")]
        public ActionResult<Usuarios> Login([FromBody] LoginRequest model)
        {
            Usuarios[] usuarios = _userController.GetUsuarios().ToArray();
            foreach (var user in usuarios)
            {
                if (user.Username == model.Username && user.Password == PasswordHelper.Hash(model.Password))
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

                    return Ok(new
                    {
                        accessToken = stringToken
                    });
                }
            }


            return Unauthorized("Email o contraseña incorrecto");
        }
        
    }

}