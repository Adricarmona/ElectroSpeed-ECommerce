namespace ElectroSpeed_server.Models.Data.Dto
{
    public class CheckTransactionRequest
    {
        public string Hash { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Value { get; set; }
    }
}
