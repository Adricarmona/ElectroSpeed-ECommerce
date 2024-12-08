using ElectroSpeed_server.Models.Data.Repositories;

namespace ElectroSpeed_server.Models.Data
{
    public class UnitOfWork
    {
        private readonly ElectroSpeedContext _context;
        private UserRepository _userRepository;
        private ReseniasRepository _reseniasRepository;
        private PedidosRepository _pedidosRepository;
        private OrdeTemporalRepository _ordeTemporalRepository; 
        private CarritoCompraRepository _carritoCompraRepository;
        private BicisCantidadRepository _bicisCantidadRepository;
        private BikeRepository _bikeRepository;

        public UserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public ReseniasRepository ReseniasRepository => _reseniasRepository ??= new ReseniasRepository(_context);
        public PedidosRepository PedidosRepository => _pedidosRepository ??= new PedidosRepository(_context);
        public OrdeTemporalRepository OrdeTemporalRepository => _ordeTemporalRepository ??= new OrdeTemporalRepository(_context);
        public CarritoCompraRepository CarritoCompraRepository => _carritoCompraRepository ??= new CarritoCompraRepository(_context);
        public BicisCantidadRepository BicisCantidadRepository => _bicisCantidadRepository ??= new BicisCantidadRepository(_context);
        public BikeRepository BikeRepository => _bikeRepository ??= new BikeRepository(_context);

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
