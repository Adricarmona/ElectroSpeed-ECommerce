using ElectroSpeed_server.Models.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class adicionResenia
    {
        public int Id { get; set; }
        public string texto { get; set; }
        public int UsuarioId { get; set; }
        public int BicicletaId { get; set; }
    }
}
