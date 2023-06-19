namespace Forum_VTB.Models
{
    public class RefreshToken
    {
        public string? Token { get; set; }

        public DateTime DateOfCreating { get; set; }

        public DateTime DateOfExpiring { get; set; }
    }
}
