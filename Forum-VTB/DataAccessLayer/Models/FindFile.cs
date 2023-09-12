namespace DataAccessLayer.Models
{
    public sealed class FindFile
    {
        public string? Id { get; set; }

        public string? FindId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Find? Find { get; set; }
    }
}
