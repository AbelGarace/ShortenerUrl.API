using ShortenerUrl.Api.ApplicationCore.Entities;

namespace ShortenerUrl.Api.ApplicationCore.Specifications
{
    public class GetLinkByShortIdSpec : BaseSpecification<Link>
    {
        public GetLinkByShortIdSpec(string shortId) : base(l => l.ShortId == shortId)
        {

        }
    }
}
