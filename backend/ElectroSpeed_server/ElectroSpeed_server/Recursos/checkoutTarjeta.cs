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

        public OrdenTemporal CogerOrdenTemporal(string reserva)
        {
            var id = Convert.ToInt32(reserva);
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.Id == id);

            //bucle para recorrer las bicicletas del carrito
             foreach (var item in orden.Bicis)
             {
                 var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici);//buscamos la bici en la base de datos
            
                 bici.Stock = bici.Stock - item.cantidad;//eliminamos el stock en funcion de la cantidad de bici seleccionadas
                _esContext.SaveChanges();
            }

            return orden;
        }

    }
}
