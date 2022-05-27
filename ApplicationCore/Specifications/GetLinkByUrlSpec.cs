using ShortenerUrl.Api.ApplicationCore.Entities;

namespace ShortenerUrl.Api.ApplicationCore.Specifications
{
    public class GetLinkByUrlSpec : BaseSpecification<Link>
    {
        public GetLinkByUrlSpec(string url) : base(l => l.Url == url)
        {

        }
    }
}
