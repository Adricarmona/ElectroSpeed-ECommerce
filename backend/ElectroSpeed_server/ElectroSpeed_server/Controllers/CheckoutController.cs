using ElectroSpeed_server.Models.Data;
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
        public async Task<ActionResult> EmbededCheckout()
        {
            //ProductDto product = GetProducts()[0];

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
                            Name = "nombre",//product.Name,
                            Description = "descripcion",//product.Description,
                            Images = ["1","2"]//[product.ImageUrl]
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

        [HttpGet("status/{sessionId}")]
        public async Task<ActionResult> SessionStatus(string sessionId)
        {
            SessionService sessionService = new SessionService();
            Session session = await sessionService.GetAsync(sessionId);

            return Ok(new { status = session.Status, customerEmail = session.CustomerEmail });
        }

        //private ProductDto[] GetProducts()
        //{
        //    return [
        //        new ProductDto
        //   {
        //       Name = "Rana epiléptica",
        //       Description = "¡Presentamos la Rana Epiléptica! El DJ del estanque que transforma cualquier charca en una fiesta con sus saltos y croac descontrolados. ¡Lleva la diversión anfibia a otro nivel!",
        //       Price = 100,
        //      ImageUrl = Request.GetAbsoluteUrl("products/frog.gif")
        //  }
        //  ];
        // }
    }
}
