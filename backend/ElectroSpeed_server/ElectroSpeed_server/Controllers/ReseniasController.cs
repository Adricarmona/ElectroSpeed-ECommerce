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

        [HttpGet]
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

        [HttpPost("/IA")]
        public ModelOutput Predict(string text)
        {
            ModelInput input = new ModelInput
            {
                Text = text
            };
            ModelOutput output = _model.Predict(input);

            return output;
        }

    }
}
