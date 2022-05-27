using ShortenerUrl.Api.ApplicationCore.Entities;

namespace ShortenerUrl.Api.ApplicationCore.Interfaces
{
    public interface ILinkService
    {
        Task<Link> GetById(Guid id);
        Task<Link> GetByShortId(string shortId);
        Task<Link> GetByUrl(string url);
        Task<Link> CreateShortLink(string url);
        Task<bool> DeleteLink(string shortId);
        Task<Link> GetUrlToRedirectByShortId(string shortId);

        Task<IEnumerable<Link>> GetAllLinks();
    }
}
