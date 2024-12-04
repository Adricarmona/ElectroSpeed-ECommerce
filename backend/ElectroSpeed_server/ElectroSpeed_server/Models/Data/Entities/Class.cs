using ElectroSpeed_server.Models.Data.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class OrdeTemporal
    {
        public int Id { get; set; }
        public IList<BicisCantidad> BicisCantidad { get; set; }

        public int UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuario { get; set; }

    }
}
