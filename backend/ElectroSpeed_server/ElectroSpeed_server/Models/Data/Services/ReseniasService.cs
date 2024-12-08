using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data.Repositories;
using Microsoft.Extensions.ML;

namespace ElectroSpeed_server.Models.Data.Services
{
    public class ReseniasService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _model;

        public ReseniasService(UnitOfWork unitOfWork, PredictionEnginePool<ModelInput, ModelOutput> model)
        {
            _unitOfWork = unitOfWork;
            _model = model;
        }

        public IList<Resenias> GetReseniasByBiciId(int biciId)
        {
            return _unitOfWork.ReseniasRepository.GetReseniasByBiciId(biciId);
        }

        public IList<Resenias> GetReseniasByUsuarioId(int usuarioId)
        {
            return _unitOfWork.ReseniasRepository.GetReseniasByUsuarioId(usuarioId);
        }

        public int GetMediaResenia(int biciId)
        {
            return _unitOfWork.ReseniasRepository.GetMediaResenia(biciId);
        }

        public void AddResenia(adicionResenia model)
        {
            ModelInput input = new ModelInput
            {
                Text = model.texto
            };
            ModelOutput output = _model.Predict(input);

            Resenias resenia = new Resenias
            {
                Id = model.Id,
                textoDeResenia = model.texto,
                resultadoResenia = output.PredictedLabel + 2,
                UsuarioId = model.UsuarioId,
                BicicletaId = model.BicicletaId
            };

            _unitOfWork.ReseniasRepository.AddResenia(resenia);
        }
    }
}
