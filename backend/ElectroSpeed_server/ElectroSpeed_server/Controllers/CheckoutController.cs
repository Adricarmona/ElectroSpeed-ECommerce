﻿using ElectroSpeed_server.Models.Data.Entities;
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

        public CheckoutController(ElectroSpeedContext esContext, IOptions<Settings> settings)
        {
            _esContext = esContext;
            _settings = settings.Value;
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

        [HttpDelete("EliminarOrdenTemporal")]
        public async Task<ActionResult> EliminarOrdenTemporal()
        {
            int idtoken = Int32.Parse(User.FindFirst("id").Value);
            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);

            var orden = checkout.CogerOrdenTemporal(idtoken);

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

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout()
        {

            // lo del puto token dios santo 4h soy subnormal ( el probrlema era del front )
            int idtoken = Int32.Parse(User.FindFirst("id").Value);

            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);

            var orden = checkout.CogerOrdenTemporal(idtoken);

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
                CustomerEmail = "correo_cliente@correo.es",
                RedirectOnCompletion = "never"
            };

            SessionService service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { clientSecret = session.ClientSecret });
        }

        [HttpGet("status/{sessionId}")]
        public async Task<ActionResult> SessionStatus(string sessionId)
        {
            SessionService sessionService = new SessionService();
            Session session = await sessionService.GetAsync(sessionId);

            return Ok(new { status = session.Status, customerEmail = session.CustomerEmail });
        }
        /*
        [HttpPost("guardarcomprar")]
        public ActionResult AnadirPedidos([FromBody] BicicletasAnadir model)
        {

            Pedidos pedido = new()
            {
                Id = model.Id,
                MarcaModelo = model.MarcaModelo,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Stock = model.Stock,
                UrlImg = model.Foto,
            };

            _esContext.Bicicletas.Add(bicicleta);
            _esContext.SaveChanges();

            return Ok("Subida correctamente");
        }*/
    }
}
