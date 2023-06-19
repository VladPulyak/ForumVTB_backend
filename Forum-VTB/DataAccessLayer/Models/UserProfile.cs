namespace DataAccessLayer.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string? NickName { get; set; }

        public string? Login { get; set; }

        public string? HashPassword { get; set; }

        public string? Email { get; set; }

        public bool IsEmailConfirm { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime DateOfCreating { get; set; }

        public DateTime DateOfExpiring { get; set; }

        public ICollection<Message>? Messages { get; set; }

        public UserRole? UserRole { get; set; }
    }
}