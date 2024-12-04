using ElectroSpeed_server.Extensions;
using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Recursos
{
    public class ImagenMapper
    {
        public IList<Bicicletas> AddCorrectPath(IList<Bicicletas> bicicletas, HttpRequest httpRequest = null)
        {
            foreach (Bicicletas bici in bicicletas)
            {
                bici.UrlImg = httpRequest is null ? bici.UrlImg : httpRequest.GetAbsoluteUrl("bicis/" + bici.UrlImg);
            }
            return bicicletas;
        }

        public Bicicletas AddCorrectPath(Bicicletas bici, HttpRequest httpRequest = null)
        {
            string test = "bicis/" + bici.UrlImg;
            bici.UrlImg = httpRequest.GetAbsoluteUrl(test);
            return bici;
        }

    }
}
