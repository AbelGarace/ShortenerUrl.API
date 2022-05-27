using ShortenerUrl.Api.ApplicationCore.Dtos;
using ShortenerUrl.Api.ApplicationCore.Entities;

namespace ShortenerUrl.Api.ApplicationCore.Interfaces
{
    public interface ILinkAssembler
    {
        LinkDto GetLinkDto(Link link);
        IEnumerable<LinkDto> GetLinkDtos(IEnumerable<Link> links);
    }
}
