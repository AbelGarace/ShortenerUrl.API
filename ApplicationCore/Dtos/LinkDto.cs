namespace ShortenerUrl.Api.ApplicationCore.Dtos
{
    public class LinkDto
    {
        public string ShortId { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public int Clicks { get; set; }
    }
}
