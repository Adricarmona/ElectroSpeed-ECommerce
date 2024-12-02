using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class BiciTemporal
    {
        public int Id { get; set; }

        public int cantidad { get; set; }
        public string MarcaModelo { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int Precio { get; set; }
        public string UrlImg { get; set; }
        public int? ReseniasId { get; set; }

        public ICollection<Resenias> Resenias { get; set; }

        public IList<BicisCantidad> BicisCantidadId { get; set; }
    }
}
