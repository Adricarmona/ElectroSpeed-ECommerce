using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    [PrimaryKey(nameof(Id))]
    public class CarritoCompra
    {
        public int Id { get; set; }
        public ICollection<Bicicletas> Bicicletas { get; set; } = new List<Bicicletas>();

        public int UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuario { get; set; }

    }
}
