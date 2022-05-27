namespace ShortenerUrl.Api.ApplicationCore.Utils
{
    public static class UriUtils
    {
        public static string baseUrl { get; set; }

        public static bool IsValidUrl(string url)
        {            
            var isValid = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            
            return isValid;
        }

    }
}
