using System.Text.Json.Serialization;

namespace BusinessLayer.Dtos.Authentication
{
    public class GoogleAuthRequestDto
    {
        [JsonPropertyName("credential")]
        public string? OAuthToken { get; set; }
    }
}
