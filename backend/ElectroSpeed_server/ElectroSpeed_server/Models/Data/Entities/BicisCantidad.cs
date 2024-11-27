using ElectroSpeed_server.Models.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class BicisCantidad
    {
        public int Id { get; set; }
        public int IdBici { get; set; } 
        public int idCarrito { get; set; }
        public int cantidad { get; set; }

        [ForeignKey(nameof(IdBici))]
        public Bicicletas bicicletas{ get; set; }

        [ForeignKey(nameof(idCarrito))]
        public CarritoCompra CarritoCompra { get; set; }
    }
}
