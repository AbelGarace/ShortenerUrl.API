using ShortenerUrl.Api.ApplicationCore.Dtos;
using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.ApplicationCore.Utils;
using ShorternerUrl.Api.Interfaces;

namespace ShorternerUrl.Api.Services
{
    public class LinkDtoService : ILinkDtoService
    {
        private readonly ILinkService _linkService;
        private readonly ILinkAssembler _linkAssembler;

        public LinkDtoService(ILinkService linkService, ILinkAssembler linkAssembler)
        {
            _linkService = linkService;
            _linkAssembler = linkAssembler;
        }

        public async Task<LinkDto> GetById(Guid id)
        {
            var link = await _linkService.GetById(id);
            var linkDto = _linkAssembler.GetLinkDto(link);
            return linkDto;
        }

        public async Task<LinkDto> GetByShortId(string shortId)
        {
            var link = await _linkService.GetByShortId(shortId);

            if (link is null)
                throw new NullReferenceException($"The Short Id {shortId} not in our database");

            var linkDto = _linkAssembler.GetLinkDto(link);
            return linkDto;
        }

        public async Task<bool> DeleteLink(string shortId)
        {
            var link = await _linkService.GetByShortId(shortId);

            if (link is null)
                throw new NullReferenceException($"The Short Id {shortId} not in our database");

            var result = await _linkService.DeleteLink(shortId);

            return result;
        }

        public async Task<LinkDto> GetByUrl(string url)
        {
            var unescapeUrl = Uri.UnescapeDataString(url);

            if (!UriUtils.IsValidUrl(unescapeUrl)) throw new NullReferenceException($"The Url {unescapeUrl} is not valid, must be Absolute.");

            var link = await _linkService.GetByUrl(unescapeUrl);

            var linkDto = new LinkDto();

            if (link is null)
            {
                link = await _linkService.CreateShortLink(unescapeUrl);
                linkDto = _linkAssembler.GetLinkDto(link);
            }
            else
            {
                linkDto = _linkAssembler.GetLinkDto(link);
            }

            return linkDto;
        }

        public async Task<LinkDto> GetUrlToRedirectByShortId(string shortId)
        {
            var link = await _linkService.GetUrlToRedirectByShortId(shortId);

            if (link is null)
                throw new NullReferenceException($"The Short Id {shortId} not in our database");

            var linkDto = _linkAssembler.GetLinkDto(link);
            return linkDto;
        }

        public async Task<IEnumerable<LinkDto>> GetAllLinks()
        {
            var links = await _linkService.GetAllLinks();

            var linkDtos = _linkAssembler.GetLinkDtos(links);

            return linkDtos;
        }
    }
}
