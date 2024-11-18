using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseniasController : ControllerBase
    {
        private ElectroSpeedContext _esContext;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _model;

        public ReseniasController(ElectroSpeedContext esContext, PredictionEnginePool<ModelInput, ModelOutput> model)
        {
            _esContext = esContext;
            _model = model;
        }

        [HttpGet("/idBici")]
        public IList<Resenias> ReseniasIdBici(int id)
        {
            RecursosResenias resenias = new(_esContext);

            return resenias.ReseniasIdBici(id);
        }

        [HttpGet("/idUsuario")]
        public IList<Resenias> ReseniasIdUsuario(int id)
        {
            RecursosResenias resenias = new(_esContext);

            return resenias.ReseniasIdUsuario(id);
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

        [HttpPost("/IA añadir")]
        public ActionResult Predict([FromBody] adicionResenia model)
        {
            ModelInput input = new ModelInput
            {
                Text = model.texto
            };
            ModelOutput output = _model.Predict(input);

            Resenias resenia = new()
            {
                Id = model.Id,
                textoDeResenia = model.texto,
                resultadoResenia = (int)output.PredictedLabel,
                UsuarioId = model.UsuarioId,
                BicicletaId = model.BicicletaId
            };

            _esContext.Resenias.Add(resenia);
            _esContext.SaveChanges();

            return Ok("Subida correctamente");
        }

    }
}
