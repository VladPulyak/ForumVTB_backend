namespace DataAccessLayer.Models
{
    public sealed class TopicFavourite
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? TopicId { get; set; }

        public UserProfile? User { get; set; }

        public Topic? Topic { get; set; }
    }
}
