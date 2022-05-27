using ShortenerUrl.Api.ApplicationCore.Dtos;

namespace ShorternerUrl.Api.Interfaces
{
    public interface ILinkDtoService
    {
        Task<LinkDto> GetByShortId(string ShortId);
        Task<bool> DeleteLink(string ShortId);
        Task<LinkDto> GetByUrl(string url);
        Task<LinkDto> GetUrlToRedirectByShortId(string shortId);
        Task<IEnumerable<LinkDto>> GetAllLinks();
    }
}
