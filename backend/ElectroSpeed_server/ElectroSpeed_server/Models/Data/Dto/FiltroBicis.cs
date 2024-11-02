namespace ElectroSpeed_server.Models.Data.Dto
{
    public class FiltroBicis
    {
        public string Consulta { get; set; }
        public Criterio Criterio { get; set; }
        public Orden Orden { get; set; }
        public int CantidadPagina { get; set; }
        public int PaginaActual { get; set; }

    }
    public enum Criterio { Marca, Precio }
    public enum Orden { Asc, Desc }
}
