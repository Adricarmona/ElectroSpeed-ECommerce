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

        public OrdenTemporal CogerOrdenTemporal(int id)
        {
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.UsuarioId == id);

            //bucle para recorrer las bicicletas del carrito
           // foreach (var item in carrito.BicisCantidad)
           // {
           //     var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici);//buscamos la bici en la base de datos
           //
           //     bici.Stock = bici.Stock - item.cantidad;//eliminamos el stock en funcion de la cantidad de bici seleccionadas
           //
           // }

            return orden;
        }

    }
}
