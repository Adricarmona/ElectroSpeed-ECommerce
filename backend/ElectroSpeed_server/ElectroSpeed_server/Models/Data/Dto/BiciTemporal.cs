using ElectroSpeed_server.Models.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class BiciTemporal
    {
        public int Id { get; set; }

        public int IdBici { get; set; }
        public int cantidad { get; set; }
    }
}
