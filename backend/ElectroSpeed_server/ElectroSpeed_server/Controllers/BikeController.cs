using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Mvc;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Recursos;
using Microsoft.EntityFrameworkCore;
using ElectroSpeed_server.Models.Data.Repositories;
using ElectroSpeed_server.Models.Data.Services;


namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly BikeService _bikeService;

        public BikeController(BikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpPost("/filtroBicis")]
        public async Task<ActionResult<BicisPaginas>> FiltroBicis([FromBody] FiltroBicis model)
        {
            var result = await _bikeService.FiltroBicisAsync(model);
            if (result == null)
            {
                return BadRequest("No se pudieron aplicar los filtros correctamente.");
            }
            return Ok(result);
        }

        [HttpPost("/anadirBici")]
        public async Task<ActionResult> AnadirBicis([FromBody] BicicletasAnadir model)
        {
            if (await _bikeService.GetBikeByIdAsync(model.Id) != null)
            {
                return BadRequest("Bici ya en la base de datos");
            }

            var bicicleta = new Bicicletas
            {
                Id = model.Id,
                MarcaModelo = model.MarcaModelo,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Stock = model.Stock,
                UrlImg = model.Foto,
            };

            await _bikeService.AddBikeAsync(bicicleta);
            return Ok("Subida correctamente");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicicletas>>> GetBicicletas()
        {
            var bicicletas = await _bikeService.GetAllBikesAsync();
            return Ok(bicicletas);
        }

        [HttpGet("/bicicleta")]
        public async Task<ActionResult<Bicicletas>> GetBicicleta(int id)
        {
            var bicicleta = await _bikeService.GetBikeByIdAsync(id);
            if (bicicleta == null)
            {
                return NotFound();
            }
            return Ok(bicicleta);
        }
    }

}
