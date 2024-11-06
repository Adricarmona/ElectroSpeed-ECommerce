using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Models.Data.Dto
{
    public class BicisPaginas
    {
        public int paginasTotales { get; set; }
        public IEnumerable<Bicicletas> Bicicletas { get; set; }
    }
}
