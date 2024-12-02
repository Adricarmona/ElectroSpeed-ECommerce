using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;

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

        [HttpPost("OrdenTemporal")]
        public async Task<ActionResult> CrearOrdentemporal(OrdenTemporal model)
        {

            OrdenTemporal ordenTemporal = new()
            {
                idUsuario = model.idUsuario,
                Bici = model.Bici
            };

            return Ok("orden creada");
        }

        [HttpGet("AllProducts")]
        public IList<Bicicletas> AllProducts()
        {
            int idtoken = Int32.Parse(User.FindFirst("id").Value);
            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);
            IList<Bicicletas> bici = checkout.AllProduct(idtoken);
            return bici;
        }

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout()
        {

            // lo del puto token dios santo 4h soy subnormal ( el probrlema era del front )
            int idtoken = Int32.Parse(User.FindFirst("id").Value);

            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);

            var orden = checkout.Ordentemporal(idtoken);

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var b in orden)
            {
                lineItems.Add(new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "eur",
                        UnitAmount = (b.Precio) * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = b.Nombre,
                            Description = b.Description,
                            Images = new List<string> { b.UrlImg }
                        }
                    },
                    Quantity = b.Cantidad
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
