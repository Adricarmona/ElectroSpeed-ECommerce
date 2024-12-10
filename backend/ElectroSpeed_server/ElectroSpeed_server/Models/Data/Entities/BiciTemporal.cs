using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class BiciTemporal
    {
        public int Id { get; set; }
        public int IdBici { get; set; }
        public int cantidad { get; set; }

        [ForeignKey(nameof(IdBici))]
        public Bicicletas bicicletas { get; set; }

    }
}
