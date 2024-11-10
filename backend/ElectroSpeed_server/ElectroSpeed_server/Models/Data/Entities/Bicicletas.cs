using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Bicicletas
    {
        public int Id { get; set; }
        public string MarcaModelo { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int Precio { get; set; }
        public string UrlImg { get; set; }
        public int ReseniasId { get; set; }

        [ForeignKey(nameof(ReseniasId))]
        public Resenias Resenias { get; set; }

    }
}
