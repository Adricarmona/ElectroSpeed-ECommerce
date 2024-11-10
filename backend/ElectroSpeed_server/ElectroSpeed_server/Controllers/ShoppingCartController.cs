using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ElectroSpeedContext _esContext;

        public ShoppingCartController(ElectroSpeedContext escontext)
        {
            _esContext = esContext;
        }
    }
}
