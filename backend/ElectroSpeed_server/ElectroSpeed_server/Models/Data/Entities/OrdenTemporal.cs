using ElectroSpeed_server.Models.Data.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    public class OrdenTemporal
    {

        public int Id { get; set; }
        public IList<BiciTemporal> Bici { get; set; }

        public int idUsuario { get; set; }
    }
}
