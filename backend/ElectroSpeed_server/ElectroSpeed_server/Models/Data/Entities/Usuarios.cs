using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Usuarios
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        public ICollection<Resenias> Resenias { get; set; }


        public static implicit operator Usuarios(int v)
        {
            throw new NotImplementedException();
        }
    }
}
