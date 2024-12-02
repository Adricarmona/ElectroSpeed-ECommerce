using ElectroSpeed_server.Models.Data.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class Pedidos
    {
        public int Id { get; set; }
        public IList<BicisCantidad> BicisCantidad { get; set; } = new List<BicisCantidad>();

        public int UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuario { get; set; }
    }
}
