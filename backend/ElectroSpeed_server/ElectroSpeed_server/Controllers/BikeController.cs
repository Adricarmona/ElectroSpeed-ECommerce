using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Mvc;
using ElectroSpeed_server.Models.Data.Dto;

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

        [HttpPost("/anadirBici")]
        public ActionResult anadirBicis([FromBody] BicicletasAnadir model)
        {
            if (_esContext.Bicicletas.Any(Bicicletas => model.Id == Bicicletas.Id))
            {
                return BadRequest("Bici ya en la base de datos");
            }

            Bicicletas bicicleta = new Bicicletas
            {
                Id = model.Id,
                Marca = model.Marca,
                Modelo = model.Modelo,
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
            return _esContext.Bicicletas.ToList();
        }
    }
}
