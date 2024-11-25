using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Authorization;
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
        public IList<Bicicletas> AllProducts(int idUsuario)
        {
            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);
            IList<Bicicletas> bici = checkout.AllProduct(idUsuario);
            return bici;
        }

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout(int idUsuario)
        {

            string token = User.FindFirst("id").Value;

            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);

            var orden = checkout.Ordentemporal(idUsuario);

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var b in orden.BicisCantidad)
            {
                var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == b.IdBici);
                    lineItems.Add(new SessionLineItemOptions()
                    {
                        PriceData = new SessionLineItemPriceDataOptions()
                        {
                            Currency = "eur",
                            UnitAmount = (long)(1 * 100),
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
                ReturnUrl = "http://localhost:4200"+"/checkout?session_id={CHECKOUT_SESSION_ID}"
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
    }
}
