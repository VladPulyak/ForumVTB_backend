namespace DataAccessLayer.Models
{
    public sealed class UserTheme
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? Theme { get; set; }

        public UserProfile? User { get; set; }
    }
}
