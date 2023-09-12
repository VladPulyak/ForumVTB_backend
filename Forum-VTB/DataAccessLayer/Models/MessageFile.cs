namespace DataAccessLayer.Models
{
    public sealed class MessageFile
    {
        public string? Id { get; set; }

        public string? MessageId { get; set; }

        public string? FileURL { get; set; }

        public UserMessage? UserMessage { get; set; }
    }
}
