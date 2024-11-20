using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ElectroSpeed_server.Recursos
{
    public class RecursosResenias
    {
        private readonly ElectroSpeedContext _electroSpeedContext;

        public RecursosResenias(ElectroSpeedContext electroSpeedContext)
        {
            _electroSpeedContext = electroSpeedContext;
        }

        public IList<Resenias> ReseniasIdBici(int id)
        {
            return _electroSpeedContext.Resenias.Where(r => r.BicicletaId == id).ToList();
        }

        public IList<Resenias> ReseniasIdUsuario(int id)
        {
            return _electroSpeedContext.Resenias.Where(r => r.UsuarioId == id).ToList();
        }

        public double MediaResenia(int id)
        {
            var reseñas = ReseniasIdBici(id);

            if (reseñas == null || !reseñas.Any())
            {
                return 0;
            }
            else
            {
                var media = reseñas.Average(r => r.resultadoResenia);
                return media;
            }
        }

    }
}
