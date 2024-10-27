using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TokenValidationParameters _tokenValidationParameter;
        private ElectroSpeedContext _esContext;

        public UserController(ElectroSpeedContext esContext) 
        {
            _esContext = esContext;
        }
        [HttpGet]
        public IEnumerable<Usuarios> GetUsuarios()
        {
            return _esContext.Usuarios;
        }

        [HttpPost]
        public ActionResult<Usuarios> Login([FromBody] LoginRequest model)
        {
            Usuarios[] usuarios = GetUsuarios().ToArray();
            foreach (var user in usuarios)
            {
                if (user.Username == model.Username && user.Password == model.Password)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Claims = new Dictionary<string, object>
                        {
                            {ClaimTypes.Role, "cliente"}
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
