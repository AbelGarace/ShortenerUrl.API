namespace ShortenerUrl.Api.ApplicationCore.Dtos.Auth
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Secret { get; set; }

        public int ExpirationInDays { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
