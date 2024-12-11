namespace ElectroSpeed_server.Models.Data.Dto
{
    public class Correo
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool ishtml { get; set; }
    }
}
