namespace ElectroSpeed_server.Models.Data.Dto
{
    public class UserToAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public Boolean Admin { get; set; }
    }
}
