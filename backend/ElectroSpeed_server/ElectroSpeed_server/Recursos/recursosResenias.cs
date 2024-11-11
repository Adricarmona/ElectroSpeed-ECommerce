using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Recursos
{
    public class RecursosResenias
    {
        private readonly ElectroSpeedContext _electroSpeedContext;

        public RecursosResenias(ElectroSpeedContext electroSpeedContext)
        {
            _electroSpeedContext = electroSpeedContext;
        }

        public IList<Resenias> cogerResenias(int id)
        {
            IList<Resenias> reseniasTotales = new List<Resenias>();

            foreach (Resenias item in _electroSpeedContext.Resenias.ToList())
            {
                reseniasTotales.Add(item);
            }
            
            return reseniasTotales;
        }
    }
}
