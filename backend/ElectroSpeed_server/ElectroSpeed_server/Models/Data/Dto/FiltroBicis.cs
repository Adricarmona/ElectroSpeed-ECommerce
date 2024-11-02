namespace ElectroSpeed_server.Models.Data.Dto
{
    public class FiltroBicis
    {
        public string consulta { get; set; }
        public enum Criterio { Nombre, Precio }
        public enum Orden { Asc, Desc}
        public int cantidadPagina { get; set; }
        public int paginaActual { get; set; }
    }
}
