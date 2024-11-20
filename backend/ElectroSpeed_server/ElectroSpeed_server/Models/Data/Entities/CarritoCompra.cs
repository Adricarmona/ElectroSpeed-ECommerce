using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class CarritoCompra
    {
        internal string id;

        public int Id { get; set; }
        public int BicicletasId { get; set; }
        public int? UsuariosId { get; set; }

        [ForeignKey(nameof(BicicletasId))]
        public Bicicletas Bicletas { get; set; }

        [ForeignKey(nameof(UsuariosId))]
        public Usuarios Usuarios { get; set; }
    }
}
