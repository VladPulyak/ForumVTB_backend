using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Authentication
{
    public class GoogleAuthRequestDto
    {
        [JsonPropertyName("credential")]
        public string? OAuthToken { get; set; }
    }
}
