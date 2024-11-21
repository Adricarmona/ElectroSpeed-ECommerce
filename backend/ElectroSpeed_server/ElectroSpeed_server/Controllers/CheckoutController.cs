using ElectroSpeed_server.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace ElectroSpeed_server.Controllers
{
    public class CheckoutController
    {
        private readonly ElectroSpeedContext _esContext;

        public CheckoutController(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout()
        {
            ProductDto product = GetProducts()[0];

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
                        UnitAmount = (long)(product.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Images = [product.ImageUrl]
                        }
                    },
                    Quantity = 1,
                },
            },
                CustomerEmail = "correo_cliente@correo.es",
                ReturnUrl = _settings.ClientBaseUrl + "/checkout?session_id={CHECKOUT_SESSION_ID}",
            };

            SessionService service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { clientSecret = session.ClientSecret });
        }
    }
}
