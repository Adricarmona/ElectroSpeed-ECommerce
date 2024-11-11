using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class biciDetalle
    {
        public Bicicletas bici { get; set; }
        public IList<Resenias> reseñas { get; set; }
    }
}
