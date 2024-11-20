using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class CarritoCompra
    {
        public int Id { get; set; }
<<<<<<< Updated upstream
        public int BicicletasId { get; set; }
        public int? UsuariosId { get; set; }
=======
        public CarritoBicisCantidad[] BicicletasId { get; set; }
        public int UsuariosId { get; set; }
>>>>>>> Stashed changes

        [ForeignKey(nameof(BicicletasId))]
        public Bicicletas Bicletas { get; set; }

        [ForeignKey(nameof(UsuariosId))]
        public Usuarios Usuarios { get; set; }
    }
}
