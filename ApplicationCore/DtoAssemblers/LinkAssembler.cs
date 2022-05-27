using ShortenerUrl.Api.ApplicationCore.Dtos;
using ShortenerUrl.Api.ApplicationCore.Entities;
using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.ApplicationCore.Utils;

namespace ShortenerUrl.Api.ApplicationCore.DtoAssemblers
{
    public class LinkAssembler : ILinkAssembler
    {
        public LinkDto GetLinkDto(Link link)
        {
            var linkDto = new LinkDto();

            if (link != null)
            {
                linkDto.Url = link.Url;
                linkDto.ShortUrl = CompositeUrl(link.ShortId);
                linkDto.ShortId = link.ShortId;
                linkDto.Clicks = link.Clicks;
            }

            return linkDto;
        }

        public IEnumerable<LinkDto> GetLinkDtos(IEnumerable<Link> links)
        {
            var linkDtos = new List<LinkDto>();

            if(links==null) return linkDtos;

            foreach (var link in links)
            {
                var linkDto = new LinkDto();

                linkDto.Url = link.Url;
                linkDto.ShortUrl = CompositeUrl(link.ShortId);
                linkDto.ShortId = link.ShortId;
                linkDto.Clicks = link.Clicks;

                linkDtos.Add(linkDto);
            }
                                        
            return linkDtos;
        }

        private string CompositeUrl(string shorId)
        {
            var baseUrl = UriUtils.baseUrl;
            return $"{baseUrl}/{shorId}";
        }
    }
}
