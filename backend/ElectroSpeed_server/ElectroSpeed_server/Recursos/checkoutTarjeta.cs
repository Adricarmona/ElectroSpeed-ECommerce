using ElectroSpeed_server.Models.Data;
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

        public OrdeTemporal Ordentemporal(int id)
        {
            //guardo el carrito del usuario
            var carrito = _esContext.CarritoCompra.Include(c => c.BicisCantidad).FirstOrDefault(r => r.UsuarioId == id);

            //creo la orden temporal
            OrdeTemporal orden = new()
            {
                BicisCantidad = carrito.BicisCantidad,
                UsuarioId = carrito.UsuarioId,
            };

            //bucle para recorrer las bicicletas del carrito
            foreach (var item in carrito.BicisCantidad)
            {
                var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici);//buscamos la bici en la base de datos
                
                bici.Stock = bici.Stock - item.cantidad;//eliminamos el stock en funcion de la cantidad de bici seleccionadas

            }

            return orden;
        }

    }
}
