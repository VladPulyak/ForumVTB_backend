namespace DataAccessLayer.Models
{
    public sealed class FindFavourite
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? FindId { get; set; }

        public UserProfile? User { get; set; }

        public Find? Find { get; set; }
    }
}
