namespace DataAccessLayer.Models
{
    public sealed class TopicFile
    {
        public string? Id { get; set; }

        public string? TopicId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Topic? Topic { get; set; }
    }
}
