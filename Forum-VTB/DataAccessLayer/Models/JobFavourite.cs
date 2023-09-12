namespace DataAccessLayer.Models
{
    public sealed class JobFavourite
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? JobId { get; set; }

        public UserProfile? User { get; set; }

        public Job? Job { get; set; }

    }
}
