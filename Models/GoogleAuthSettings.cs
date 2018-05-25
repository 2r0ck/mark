using Newtonsoft.Json;

namespace DotNetGigs.Models
{
    public  class GoogleAuthSettings
    {
        [JsonProperty("web")]
        public Web Web { get; set; }
    }
    public  class Web
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("auth_uri")]
        public string AuthUri { get; set; }

        [JsonProperty("token_uri")]
        public string TokenUri { get; set; }

        [JsonProperty("auth_provider_x509_cert_url")]
        public string AuthProviderX509CertUrl { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("redirect_uris")]
        public string[] RedirectUris { get; set; }

        [JsonProperty("javascript_origins")]
        public string[] JavascriptOrigins { get; set; }
    }
 
}