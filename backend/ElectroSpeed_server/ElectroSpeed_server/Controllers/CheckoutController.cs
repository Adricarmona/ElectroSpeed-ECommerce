using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;
using ElectroSpeed_server.Models.Data.Dto;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ElectroSpeedContext _esContext;
        private readonly Settings _settings;
        private readonly EmailHelper _emailHelper;

        public CheckoutController(ElectroSpeedContext esContext, IOptions<Settings> settings)
        {
            _esContext = esContext;
            _settings = settings.Value;
            _emailHelper = new EmailHelper();
        }

        [HttpPost("OrdenTemporalLocal/{carrito}")]
        public int CrearOrdentemporalLocal(string carrito)
        {
           String[] idBicis = carrito.Split(",");

            Dictionary<int, int> cantidad = new();
            foreach (var item in idBicis)
            {
                int id = Convert.ToInt32(item);
                if (cantidad.ContainsKey(id))
                {
                    cantidad[id]++;
                }
                else
                {
                    cantidad[id] = 1;
                }
            }

            IList<BiciTemporal> biciT = [];

            foreach (var item in cantidad)
            {
                BiciTemporal b = new()
                {
                    IdBici = item.Key,
                    cantidad = item.Value
                };
                biciT.Add(b);
            }

            OrdenTemporal ordenTemporal = new()
            {
                UsuarioId = 1,
                Bicis = biciT
            };

            _esContext.OrdenTemporal.Add(ordenTemporal);
            _esContext.SaveChanges();

            return ordenTemporal.Id;
        }

        [HttpPost("OrdenTAñadirUsuario/{reserva}")]
        public async Task<ActionResult> OrdenTAñadirUsuario(int reserva)
        {

            int idtoken = Int32.Parse(User.FindFirst("id").Value);
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.Id == reserva);
            orden.UsuarioId = idtoken;

            _esContext.SaveChanges();
            return Ok("Usuario añadido");
        }

        [HttpDelete("EliminarOrdenTemporal/{reserva}")]
        public async Task<ActionResult> EliminarOrdenTemporal(string reserva)
        {
            var id = Convert.ToInt32(reserva);
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.Id == id);

            _esContext.OrdenTemporal.Remove(orden);       
            _esContext.SaveChanges();
            return Ok("Orden eliminada");
        }


        [HttpPost("OrdenTemporalCarrito/{id}")]
        public int CrearOrdentemporalCarrito(int id)
        {
            //guardo el carrito del usuario
            var carrito = _esContext.CarritoCompra.Include(c => c.BicisCantidad).FirstOrDefault(r => r.UsuarioId == id);

            IList<BiciTemporal> biciT = [];

            foreach (var item in carrito.BicisCantidad)
            {
                BiciTemporal b = new()
                {
                    IdBici = item.IdBici,
                    cantidad = item.cantidad
                };
                biciT.Add(b);
            }

            //creo la orden temporal
            OrdenTemporal ordenTemporal = new()
            {
                Bicis = biciT,
                UsuarioId = carrito.UsuarioId,
            };

            _esContext.OrdenTemporal.Add(ordenTemporal);
            _esContext.SaveChanges();

            return ordenTemporal.Id;
        }

        [HttpGet("embedded/{reserva}")]
        public async Task<ActionResult> EmbededCheckout(string reserva)
        {

            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);

            var orden = checkout.CogerOrdenTemporal(reserva);
            var user = _esContext.Usuarios.FirstOrDefault(r => r.Id == orden.UsuarioId);

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var b in orden.Bicis)
            {
                var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == b.IdBici);
                lineItems.Add(new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "eur",
                        UnitAmount = (bici.Precio) * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = bici.MarcaModelo,
                            Description = bici.Descripcion,
                            Images = new List<string> { bici.UrlImg }
                        }
                    },
                    Quantity = b.cantidad
                });
            }

            SessionCreateOptions options = new SessionCreateOptions
            {
                UiMode = "embedded",
                Mode = "payment",
                PaymentMethodTypes = ["card"],
                LineItems = lineItems,
                CustomerEmail = user.Email,
                RedirectOnCompletion = "never"
            };

            SessionService service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { sesionid = session.Id, clientSecret = session.ClientSecret });
        }

        [HttpGet("status/{sessionId}")]
        public async Task<ActionResult> SessionStatus(string sessionId)
        {
            SessionService sessionService = new SessionService();
            Session session = await sessionService.GetAsync(sessionId);
            if (session.PaymentStatus == "paid")
            {
                EmailHelper.SendEmailAsync(session.CustomerEmail, "Compra en ElectroSpeed", "hectornegro", true);
                Console.WriteLine("pago completado");
            }

            return Ok(new { status = session.Status, customerEmail = session.CustomerEmail });
        }
        
        [HttpPost("guardarcomprar/{reserva}")]
        public ActionResult AnadirPedidos(string reserva)
        {
            var id = Convert.ToInt32(reserva);
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.Id == id);

            Pedidos pedido = new()
            {
                UsuarioId = orden.UsuarioId,
                Bicis = orden.Bicis,
            };

            _esContext.Pedido.Add(pedido);
            _esContext.SaveChanges();

            return Ok("Subida correctamente");
        }

        [HttpPost("RestaurarStock/{reserva}")]
        public ActionResult RestaurarStock(string reserva)
        {
            var id = Convert.ToInt32(reserva);
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.Id == id);

            foreach (var item in orden.Bicis)
            {
                var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici);//buscamos la bici en la base de datos

                bici.Stock = bici.Stock + item.cantidad;//añadimos el stock en funcion de la cantidad de bici seleccionadas
                _esContext.SaveChanges();
            }
            return Ok("Subida correctamente");
        }

        [HttpPost("eliminarDelCarrito/{reserva}")]
        public ActionResult EliminarDelCarrito(string reserva)
        {
            // Obtener el ID del usuario desde el token
            int idtoken = Int32.Parse(User.FindFirst("id").Value);

            // Obtener el carrito del usuario incluyendo las bicicletas
            var carrito = _esContext.CarritoCompra.Include(c => c.BicisCantidad).FirstOrDefault(c => c.UsuarioId == idtoken);

            // Obtener la orden temporal incluyendo las bicicletas
            var id = Convert.ToInt32(reserva);
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.Id == id);

            if (carrito == null || orden == null)
            {
                return BadRequest("Carrito o orden no encontrados.");
            }

            // Iterar por las bicicletas de la orden y eliminarlas del carrito
            foreach (var biciOrden in orden.Bicis)
            {
                var biciEnCarrito = carrito.BicisCantidad.FirstOrDefault(b => b.IdBici == biciOrden.IdBici);

                if (biciEnCarrito != null)
                {
                    carrito.BicisCantidad.Remove(biciEnCarrito);
                }
            }

            _esContext.SaveChanges();

            return Ok("Bicicletas eliminadas del carrito correctamente.");
        }

        [HttpPost("DevolverOrden/{reserva}")]
        public IList<Bicicletas> DevolverOrden(string reserva)
        {
            var id = Convert.ToInt32(reserva);
            var orden = _esContext.OrdenTemporal.Include(o => o.Bicis).FirstOrDefault(o => o.Id == id);

            IList<Bicicletas> bici = [];
            foreach (var item in orden.Bicis)
            {
                bici.Add(_esContext.Bicicletas.FirstOrDefault(r => r.Id == item.IdBici));
            }
            return bici;
        }

    }
}
