using ElectroSpeed_server.Models.Data.Repositories;

namespace ElectroSpeed_server.Models.Data
{
    public class UnitOfWork
    {
        private readonly ElectroSpeedContext _context;
        private UserRepository _userRepository;

        public UserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public UnitOfWork(ElectroSpeedContext context) 
        {
            _context = context; 
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
