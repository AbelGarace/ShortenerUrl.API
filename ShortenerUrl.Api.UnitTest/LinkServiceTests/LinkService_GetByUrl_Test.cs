using FluentAssertions;
using Moq;
using ShortenerUrl.Api.ApplicationCore.Entities;
using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.ApplicationCore.Services;
using ShortenerUrl.Api.ApplicationCore.Specifications;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ShortenerUrl.Api.UnitTest.LinkServiceTests
{

    public class LinkService_GetByUrl_Test
    {
        private readonly Mock<IRepository<Link>> _linksRepositoryMock;
        private readonly LinkService _linkService;
        private readonly Link _expectedResult;
        private readonly string _url;
        private readonly Link _link;

        public LinkService_GetByUrl_Test()
        {
            _url = "https://wwww.google.es";

            _link = new Link
            {
                Id = Guid.Empty,
                Url = "https://wwww.google.es",
                ShortId = "Q6e7uu",
                LastAccessDate = DateTime.Now.Date,
                RequestId = 1,
                Clicks = 0
            };

            _linksRepositoryMock = new Mock<IRepository<Link>>();
            _linksRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ISpecification<Link>>()))
                .ReturnsAsync(_link);

            var linkRepository = _linksRepositoryMock.Object;

            _linkService = new LinkService(linkRepository);

            _expectedResult = new Link
            {
                Id = Guid.Empty,
                Url = "https://wwww.google.es",
                ShortId = "Q6e7uu",
                LastAccessDate = DateTime.Now.Date,
                RequestId = 1,
                Clicks = 0
            };
        }

        [Fact]
        public async Task Given_ExistingPriceBandId_Should_DeleteIt_AndShould_VerifyRelatedDependencies()
        {
            var result = await _linkService.GetByUrl(_url);
            result.Should().BeEquivalentTo(_expectedResult);
            _linksRepositoryMock.Verify(r => r.FirstOrDefaultAsync(It.IsAny<GetLinkByUrlSpec>()), Times.Once);
        }
    }
}
