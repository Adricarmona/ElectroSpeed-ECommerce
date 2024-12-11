using ElectroSpeed_server.Models.Data.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class Pedidos
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public IList<BiciTemporal> Bicis { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuario { get; set; }

        public DateTime Fecha { get; set; }
    }
}
