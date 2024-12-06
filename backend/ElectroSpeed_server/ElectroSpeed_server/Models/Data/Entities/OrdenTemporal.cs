using ElectroSpeed_server.Models.Data.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class OrdenTemporal
    {

        public int Id { get; set; }
        public IList<Bicicletas> Bici { get; set; }

        public int UsuarioId { get; set; }

        public IList<BicisCantidad> BicisCantidad { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuario { get; set; }
    }
}
