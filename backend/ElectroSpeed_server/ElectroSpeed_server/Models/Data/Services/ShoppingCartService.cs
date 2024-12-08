using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data.Repositories;
using Org.BouncyCastle.Crypto.Tls;

namespace ElectroSpeed_server.Models.Data.Services
{
    public class ShoppingCartService
    {
        private readonly UnitOfWork _unitOfWork;

        public ShoppingCartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<CarritoCompra>> GetAllCarritos()
        {
            return _unitOfWork.CarritoCompraRepository.GetAllCarritos();
        }

        public Task<CarritoCompra> GetCarritoByUserId(int userId)
        {
            return _unitOfWork.CarritoCompraRepository.GetCarritoByUserId(userId);
        }

        public Task<BicisCantidad> AddBiciToCarrito(int carritoId, int biciId)
        {
            return _unitOfWork.CarritoCompraRepository.AddBiciToCarrito(carritoId, biciId);
        }

        public Task<BicisCantidad> RemoveBiciFromCarrito(int carritoId, int biciId)
        {
            return _unitOfWork.CarritoCompraRepository.RemoveBiciFromCarrito(carritoId, biciId);
        }
        public async Task ClearCarrito(int carritoId)
        {
            var carrito = await _unitOfWork.CarritoCompraRepository.GetCarritoByUserId(carritoId);

            if (carrito != null)
            {
                carrito.BicisCantidad.Clear();
            }
        }

    }
}
