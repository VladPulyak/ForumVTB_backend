namespace DataAccessLayer.Models
{
    public sealed class Topic
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public string? UserId { get; set; }

        public int? SubsectionId { get; set; }

        public string? MainPhoto { get; set; }

        public string? Description { get; set; }

        public DateTime DateOfCreation { get; set; }

        public ICollection<TopicMessage>? Messages { get; set; }

        public ICollection<TopicFile>? Files { get; set; }

        public UserProfile? User { get; set; }

        public Subsection? Subsection { get; set; }

        public ICollection<TopicFavourite>? Favourites { get; set; }

    }
}
