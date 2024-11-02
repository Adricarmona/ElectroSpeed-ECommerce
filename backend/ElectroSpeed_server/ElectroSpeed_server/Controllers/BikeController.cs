using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Mvc;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Recursos;
using Microsoft.EntityFrameworkCore;


namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private ElectroSpeedContext _esContext;

        public BikeController(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }

        [HttpPost("/filtroBicis")]
        public IEnumerable<Bicicletas> FiltroBicis([FromBody] FiltroBicis model)
        {
            FiltroRecurso filtro = new FiltroRecurso(_esContext);

            return filtro.Search(model.Consulta, filtro.Order(model));
        }

        [HttpPost("/anadirBici")]
        public ActionResult AnadirBicis([FromBody] BicicletasAnadir model)
        {
            if (_esContext.Bicicletas.Any(Bicicletas => model.Id == Bicicletas.Id))
            {
                return BadRequest("Bici ya en la base de datos");
            }

            Bicicletas bicicleta = new()
            {
                Id = model.Id,
                MarcaModelo = model.MarcaModelo,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Stock = model.Stock,
                UrlImg = new string[] {model.Foto},
            };

            _esContext.Bicicletas.Add(bicicleta);
            _esContext.SaveChanges();
            
            return Ok("Subida correctamente");
        }

        [HttpGet]
        public IEnumerable<Bicicletas> GetBicicletas()
        {
            return [.. _esContext.Bicicletas];
        }

    }
}
