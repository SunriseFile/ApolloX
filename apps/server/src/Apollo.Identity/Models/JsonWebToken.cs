using System;

using Newtonsoft.Json;

namespace Apollo.Identity.Models
{
    public class JsonWebToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public DateTime ExpiresIn { get; set; }
    }
}
