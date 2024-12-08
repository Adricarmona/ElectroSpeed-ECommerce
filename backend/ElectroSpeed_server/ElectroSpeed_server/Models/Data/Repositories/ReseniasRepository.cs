using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class ReseniasRepository : esRepository<Resenias>
    {
        private readonly ElectroSpeedContext _context;

        public ReseniasRepository(ElectroSpeedContext context) :base(context) 
        {
            _context = context;
        }

        public IList<Resenias> GetReseniasByBiciId(int biciId)
        {
            return _context.Resenias.Where(r => r.BicicletaId == biciId).ToList();
        }

        public IList<Resenias> GetReseniasByUsuarioId(int usuarioId)
        {
            return _context.Resenias.Where(r => r.UsuarioId == usuarioId).ToList();
        }

        public int GetMediaResenia(int biciId)
        {
            return (int)_context.Resenias.Where(r => r.BicicletaId == biciId).Average(r => r.resultadoResenia);
        }

        public void AddResenia(Resenias resenia)
        {
            _context.Resenias.Add(resenia);
        }
    }

}
