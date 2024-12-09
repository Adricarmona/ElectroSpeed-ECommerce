using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Mvc;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Recursos;
using Microsoft.EntityFrameworkCore;
using ElectroSpeed_server.Models.Data.Repositories;
using ElectroSpeed_server.Helpers;


namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private ElectroSpeedContext _esContext;
        private ImagenMapper _imagenMapper;

        public BikeController(ElectroSpeedContext esContext, ImagenMapper imagenMapper)
        {
            _esContext = esContext;
            _imagenMapper = imagenMapper;
        }

        [HttpPost("/filtroBicis")]
        public BicisPaginas FiltroBicis([FromBody] FiltroBicis model)
        {
            FiltroRecurso filtro = new FiltroRecurso(_esContext);

            IList<Bicicletas> busqueda = filtro.Search(model.Consulta, _esContext.Bicicletas.ToList());
            busqueda = filtro.Order(model, busqueda);
            BicisPaginas paginasFiltradas = filtro.Pages(model, busqueda);

            // para la url de las imagenes
            paginasFiltradas.Bicicletas = _imagenMapper.AddCorrectPath(paginasFiltradas.Bicicletas,Request);

            /// Le pasamos el filtro de buscar por nombre con todas las bicicletas, luego por el de ordenacion y para terminar lo metemos en el de paginacion           
            return paginasFiltradas;
        }

        [HttpPost("/anadirBici")]
        public async Task<ActionResult> AnadirBicisAsync(BicicletaFileFoto model)
        {

            string direccionImagen = $"{Guid.NewGuid()}_{model.UrlImg.FileName}";

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
                UrlImg = direccionImagen,
            };

            using Stream stream = model.UrlImg.OpenReadStream();

            await FileHelper.SaveAsync(stream, "bicis"+direccionImagen);

            _esContext.Bicicletas.Add(bicicleta);
            _esContext.SaveChanges();
            
            return Ok("Subida correctamente");
        }

        [HttpPost("/editarBici")]
        public async Task<ActionResult> EditarBiciAsync(BicicletaFileFoto model)
        {
            Bicicletas bicicleta = getBicicleta(model.Id);

            string direccionImagen = $"{Guid.NewGuid()}_{model.UrlImg.FileName}";

            if (bicicleta == null)
            {
                return BadRequest("Bici no existe");
            }

            bicicleta.MarcaModelo = model.MarcaModelo;
            bicicleta.Descripcion = model.Descripcion;
            bicicleta.Stock = model.Stock;
            bicicleta.Precio = model.Precio;
            bicicleta.UrlImg = direccionImagen;

            using Stream stream = model.UrlImg.OpenReadStream();

            await FileHelper.SaveAsync(stream, "bicis" + direccionImagen);

            _esContext.SaveChanges();

            return Ok("Subida correctamente");
        }

        [HttpGet]
        public IList<Bicicletas> GetBicicletas()
        {
            return _imagenMapper.AddCorrectPath(_esContext.Bicicletas.ToList(), Request);
        }

        [HttpGet("/bicicleta")]
        public Bicicletas getBicicleta(int id)
        {
            Bicicletas bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == id);
            if (bici == null)
            {
                return bici;
            }
            
            return _imagenMapper.AddCorrectPath(bici, Request);

        }

        [HttpDelete("/deleteBikeId")]
        public async Task<ActionResult> DeleteBike(int id)
        {
            Bicicletas bicicleta = getBicicleta(id);

            if (bicicleta == null)
            {
                return NotFound("No se encuentra la bicicleta");
            }

            _esContext.Remove(bicicleta);
            await _esContext.SaveChangesAsync();

            return Ok("bici eliminada");
        }

    }
}
