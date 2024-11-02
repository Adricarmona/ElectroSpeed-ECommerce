using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Mvc;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Recursos;


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

        [HttpGet("/filtroBicis")]
        public IEnumerable<Bicicletas> FiltroBicis([FromBody] FiltroBicis model)
        {
            IEnumerable<Bicicletas> result;

            // Si la consulta está vacía o solo tiene espacios en blanco, devolvemos todos los items
            if (string.IsNullOrWhiteSpace(model.consulta))
            {
                result = GetBicicletas();
            }
            else
            {
                // Limpiamos la query y la separamos por espacios
                string[] queryKeys = GetKeys(ClearText(query));
                // Aquí guardaremos los items que coincidan
                List<string> matches = new List<string>();

                foreach (Bicicletas item in GetBicicletas())
                {
                    // Limpiamos el item y lo separamos por espacios
                    string[] itemKeys = GetKeys(ClearText(item.Modelo));

                    // Si coincide alguna de las palabras de item con las de query
                    // entonces añadimos item a la lista de coincidencias
                    if (IsMatch(queryKeys, itemKeys))
                    {
                        matches.Add(item.Modelo);
                    }
                }

                result = matches;
            }


            return GetBicicletas();
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
