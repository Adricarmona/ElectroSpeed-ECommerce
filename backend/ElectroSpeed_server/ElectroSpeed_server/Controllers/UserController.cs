using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
                    return Ok(user);
                }
            }


            return Unauthorized("Email o contraseña incorrecto");
        }
    }

    
}
