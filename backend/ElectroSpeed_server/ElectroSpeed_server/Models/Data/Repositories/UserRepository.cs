using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class UserRepository : esRepository<Usuarios>
    {
        private readonly ElectroSpeedContext _context;

        public UserRepository(ElectroSpeedContext context) :base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuarios>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios.Include(u => u.carritos).ToListAsync();
        }

        public async Task<Usuarios> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.carritos).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuarios> GetUsuarioByEmailAsync(string email)
        {
            return await _context.Usuarios.Include(u => u.carritos).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUsuarioAsync(Usuarios usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task UpdateUsuarioAsync(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
        }
    }

}
