﻿namespace ElectroSpeed_server.Models.Data.Dto
{
    public class BicicletaFileFoto
    {
        public int Id { get; set; }
        public string MarcaModelo { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int Precio { get; set; }
        public IFormFile UrlImg { get; set; }
    }
}
