﻿using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Mvc;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Recursos;
using Microsoft.EntityFrameworkCore;
using ElectroSpeed_server.Models.Data.Repositories;


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
        public BicisPaginas FiltroBicis([FromBody] FiltroBicis model)
        {
            FiltroRecurso filtro = new FiltroRecurso(_esContext);

            IList<Bicicletas> busqueda = filtro.Search(model.Consulta, _esContext.Bicicletas.ToList());
            busqueda = filtro.Order(model, busqueda);
            BicisPaginas paginasFiltradas = filtro.Pages(model, busqueda);

            /// Le pasamos el filtro de buscar por nombre con todas las bicicletas, luego por el de ordenacion y para terminar lo metemos en el de paginacion           
            return paginasFiltradas;
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
                UrlImg = model.Foto,
            };

            _esContext.Bicicletas.Add(bicicleta);
            _esContext.SaveChanges();
            
            return Ok("Subida correctamente");
        }

        [HttpGet]
        public IList<Bicicletas> GetBicicletas()
        {
            return _esContext.Bicicletas.ToList();
        }

        [HttpGet("/bicicleta")]
        public Bicicletas getBicicleta(int id)
        {
            return _esContext.Bicicletas.FirstOrDefault(r => r.Id == id);
        }

    }
}
