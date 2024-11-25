using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;

namespace ElectroSpeed_server.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ElectroSpeedContext _esContext;
        private readonly Settings _settings;

        public CheckoutController(ElectroSpeedContext esContext, IOptions<Settings> settings)
        {
            _esContext = esContext;
            _settings = settings.Value;
        }

        [HttpGet("AllProducts")]
        public IList<Bicicletas> AllProducts()
        {
            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);
            IList<Bicicletas> bici = checkout.AllProduct(1);
            return bici;
        }

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout()
        {
            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);

            var orden = checkout.Ordentemporal(1);

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var b in orden.BicisCantidad)
            {
                var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == b.IdBici);
                    lineItems.Add(new SessionLineItemOptions()
                    {
                        PriceData = new SessionLineItemPriceDataOptions()
                        {
                            Currency = "eur",
                            UnitAmount = (bici.Precio)*100,
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

        [HttpGet("guardarcomprar")]
    }
}
