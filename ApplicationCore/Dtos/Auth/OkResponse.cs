namespace ShortenerUrl.Api.ApplicationCore.Dtos.Auth
{
    public class OkResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = String.Empty;        
    }
}
