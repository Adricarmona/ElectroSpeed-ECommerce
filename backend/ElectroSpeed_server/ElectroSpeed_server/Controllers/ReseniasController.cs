using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data.Services;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseniasController : ControllerBase
    {
        private readonly ReseniasService _reseniasService;

        public ReseniasController(ReseniasService reseniasService)
        {
            _reseniasService = reseniasService;
        }

        [HttpGet("/idBici")]
        public IList<Resenias> ReseniasIdBici(int id)
        {
            return _reseniasService.GetReseniasByBiciId(id);
        }

        [HttpGet("/idUsuario")]
        public IList<Resenias> ReseniasIdUsuario(int id)
        {
            return _reseniasService.GetReseniasByUsuarioId(id);
        }

        [HttpGet("/mediaResenia")]
        public int MediaResenia(int id)
        {
            return _reseniasService.GetMediaResenia(id);
        }

        [HttpPost("/IAanadir")]
        public ActionResult Predict([FromBody] adicionResenia model)
        {
            _reseniasService.AddResenia(model);
            return Ok("Subida correctamente");
        }
    }
}
