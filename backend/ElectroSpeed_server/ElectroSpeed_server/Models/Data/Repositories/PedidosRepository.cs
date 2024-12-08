using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class PedidosRepository : esRepository<Pedidos>
    {
        private readonly ElectroSpeedContext _context;

        public PedidosRepository(ElectroSpeedContext context) :base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedidos>> GetAllPedidosAsync()
        {
            return await _context.Pedidos.Include(p => p.BicisCantidad).ToListAsync();
        }

        public async Task<Pedidos> GetPedidoByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.BicisCantidad).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPedidoAsync(Pedidos pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePedidoAsync(Pedidos pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePedidoAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }

}
