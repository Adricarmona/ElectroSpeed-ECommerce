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

        /*[HttpGet]
        public IList<Resenias> ReseniasId(int id)
        {
            RecursosResenias resenias = new(_esContext);

            return resenias.ReseniasId(id);
        }

        [HttpGet("/bicicleta")]
        public Bicicletas Bicicleta(int id)
        {
            return _esContext.Bicicletas.FirstOrDefault(r => r.Id == id);
        }

        [HttpGet("/mediaResenia")]
        public double MediaResenia(int id)
        {
            RecursosResenias resenias = new(_esContext);
            return resenias.MediaResenia(id);
        }

    }
}
