namespace DataAccessLayer.Models
{
    public sealed class UserChat
    {
        public string? Id { get; set; }

        public string? FirstUserId { get; set; }

        public string? SecondUserId { get; set; }

        public string? AdvertId { get; set; }

        public string? ChapterName { get; set; }

        public UserProfile? FirstUser { get; set; }

        public UserProfile? SecondUser { get; set; }

        public ICollection<UserMessage>? Messages { get; set; }
    }
}
