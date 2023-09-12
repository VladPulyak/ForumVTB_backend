namespace DataAccessLayer.Models
{
    public sealed class JobFile
    {
        public string? Id { get; set; }

        public string? JobId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Job? Job { get; set; }
    }
}
