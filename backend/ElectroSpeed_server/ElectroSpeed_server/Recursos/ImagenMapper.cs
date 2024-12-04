using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Recursos
{
    public class ImagenMapper
    {
        public IList<Bicicletas> AddCorrectPath(IList<Bicicletas> bicicletas)
        {
            foreach (Bicicletas bici in bicicletas)
            {
                bici.UrlImg = "bicis/" + bici.UrlImg;
            }
            return bicicletas;
        }

        public Bicicletas AddCorrectPath(Bicicletas bici)
        {
            bici.UrlImg = "bicis/" + bici.UrlImg;
            return bici;
        }
    }
}
