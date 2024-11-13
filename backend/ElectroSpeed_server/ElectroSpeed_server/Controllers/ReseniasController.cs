using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Mvc;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseniasController : Controller
    {
        private ElectroSpeedContext _esContext;

        public ReseniasController(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }
/*
        [HttpGet]
        public IList<Resenias> ReseniasId(int id)
        {
            return RecursosResenias
        }*/
    }
}
