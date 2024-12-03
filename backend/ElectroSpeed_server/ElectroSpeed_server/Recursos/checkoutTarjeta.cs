using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Recursos
{
    public class CheckoutTarjeta
    {
        private readonly ElectroSpeedContext _esContext;

        public CheckoutTarjeta(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }

        public IList<Bicicletas> AllProduct(int id)
        {
            var carrito = _esContext.CarritoCompra.Include(c => c.BicisCantidad).FirstOrDefault(r => r.UsuarioId == id);

            IList<Bicicletas> bici = [];

            foreach (var item in carrito.BicisCantidad)
            {
                bici.Add(_esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici));
            }

            return bici;
        }

        public IList<BicisTemporales> Ordentemporal(int id)
        {
            //guardo el carrito del usuario
            var orden = _esContext.ordenTemporal.FirstOrDefault(r => r.idUsuario == id);

            foreach (var item in orden.Bici)
            {
                var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici);

                bici.Stock = bici.Stock - item.cantidad;
            }

            IList<BicisTemporales> bicis = [];

            foreach (var item in orden.Bici)
            {
                var b = _esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici);

                BicisTemporales bicitemp = new()
                {
                    Id = b.Id,
                    Nombre = b.MarcaModelo,
                    Description = b.Descripcion,
                    Cantidad = item.cantidad,
                    Precio = b.Precio,
                    UrlImg = b.UrlImg
                };
                bicis.Add(bicitemp);
            }

            return bicis;
        }

    }
}
