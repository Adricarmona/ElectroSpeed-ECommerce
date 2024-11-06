using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Resenias
    {
        public int Id { get; set; }
        public string textoDeResenia { get; set; }
        public int resultadoResenia { get; set; }
        public int UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuarios { get; set; }
    }
}
