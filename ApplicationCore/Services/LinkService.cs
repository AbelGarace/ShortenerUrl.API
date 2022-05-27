using Microsoft.EntityFrameworkCore;
using ShortenerUrl.Api.ApplicationCore.Entities;
using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.ApplicationCore.Specifications;
using ShortenerUrl.Api.ApplicationCore.Utils;

namespace ShortenerUrl.Api.ApplicationCore.Services
{
    public class LinkService : ILinkService
    {
        private readonly IRepository<Link> _linksRepository;
        public LinkService(IRepository<Link> linksRepository)
        {
            _linksRepository = linksRepository;
        }

        public async Task<Link> GetById(Guid id)
        {
            var link = await _linksRepository.GetByIdAsync(id);
            return link;
        }

        public async Task<Link> GetByShortId(string shortId)
        {
            var spec = new GetLinkByShortIdSpec(shortId);
            var link = await _linksRepository.FirstOrDefaultAsync(spec);

            return link;
        }

        public async Task<Link> GetByUrl(string url)
        {
            var spec = new GetLinkByUrlSpec(url);
            var link = await _linksRepository.FirstOrDefaultAsync(spec);

            return link;
        }

        public async Task<Link> CreateShortLink(string url)
        {
            if (!UriUtils.IsValidUrl(url))
            {
                throw new InvalidOperationException(url);
            }

            var link = await CreateLink(url);
            return link;
        }
        public async Task<bool> DeleteLink(string shortId)
        {
          
            var result = await DeleteLinkEntity(shortId);
            return result;
        }

        public async Task<Link> GetUrlToRedirectByShortId(string shortId)
        {
            var link = await UpdateLinkClick(shortId);
            return link;
        }

        public async Task<IEnumerable<Link>> GetAllLinks()
        {
            var links = await _linksRepository.ListAllAsync();
            return links;
        }

        private async Task<Link> CreateLink(string url)
        {
            try
            {
                var nextRequestId = await GetNextRequestId();
                var link = new Link();
                link.Url = url;
                link.RequestId = nextRequestId;
                link.ShortId = ShortLinkGenerator.ValueToShort(nextRequestId);
                link.Clicks = 0;                

                var createLink = await _linksRepository.AddAsync(link);
                return createLink;
            }
            catch (DbUpdateException exception)
            {
                throw exception;
            }
        }

        private async Task<bool> DeleteLinkEntity(string shortId)
        {
            try
            {
                var spec = new GetLinkByShortIdSpec(shortId);
                var link = await _linksRepository.FirstOrDefaultAsync(spec);


                await _linksRepository.DeleteAsync(link);

                return true;
            }
            catch (DbUpdateException exception)
            {
                throw exception;
            }
        }

        private async Task<Link> UpdateLinkClick(string shortId)
        {
            try
            {
                var spec = new GetLinkByShortIdSpec(shortId);
                var link = await _linksRepository.FirstOrDefaultAsync(spec);

                link.Clicks++;

                await _linksRepository.UpdateAsync(link);
                return link;
            }
            catch (DbUpdateException exception)
            {
                throw exception;
            }
        }

        private async Task<int> GetNextRequestId()
        {
            var links = await _linksRepository.ListAllAsync();
            var total = links.Count();

            return total + 1;
        }

    }
}
