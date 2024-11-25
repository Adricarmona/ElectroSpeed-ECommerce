using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe.Forwarding;

namespace ElectroSpeed_server.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ElectroSpeedContext _esContext;

        public CheckoutController(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout(int idUsuario)
        {
            //ProductDto product = GetProducts()[0];
            CheckoutTarjeta checkout = new CheckoutTarjeta(_esContext);

            var orden = checkout.Ordentemporal(idUsuario);
            foreach (var b in orden.BicisCantidad)
            {
                var bici = _esContext.Bicicletas.FirstOrDefault(r => r.Id == b.IdBici);

                SessionCreateOptions options = new SessionCreateOptions
                {
                    UiMode = "embedded",
                    Mode = "payment",
                    PaymentMethodTypes = ["card"],
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "eur",
                        UnitAmount = (long)(1 * 100),//(product.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {

                            Name = bici.MarcaModelo,
                            Description = bici.Descripcion,
                            Images = [bici.UrlImg]
                        }
                    },
                    Quantity = 1,
                },
            },
                    CustomerEmail = "correo_cliente@correo.es",
                };


                SessionService service = new SessionService();
                Session session = await service.CreateAsync(options);

                return Ok(new { clientSecret = session.ClientSecret });
            }
            return null;
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
