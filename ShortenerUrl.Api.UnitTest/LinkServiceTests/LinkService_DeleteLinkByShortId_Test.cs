using FluentAssertions;
using Moq;
using ShortenerUrl.Api.ApplicationCore.Entities;
using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.ApplicationCore.Services;
using ShortenerUrl.Api.ApplicationCore.Specifications;
using ShortenerUrl.Api.UnitTest.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ShortenerUrl.Api.UnitTest.LinkServiceTests
{
    public class LinkService_DeleteLinkByShortId_Test
    {
        private readonly Mock<IRepository<Link>> _linksRepositoryMock;
        private readonly LinkService _linkService;
        private readonly string _shortId;
        private readonly Link _link;

        public LinkService_DeleteLinkByShortId_Test()
        {
            _shortId = "Q6e7uu";

            _link = new Link
            {
                Id = Guid.Empty,
                Url = "https://wwww.google.es",
                ShortId = "Q6e7uu",
                LastAccessDate = DateTime.Now,
                RequestId = 1,
                Clicks = 0
            };

            _linksRepositoryMock = new Mock<IRepository<Link>>();
            _linksRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ISpecification<Link>>()))
                .ReturnsAsync(_link);

            var linkRepository = _linksRepositoryMock.Object;

            _linkService = new LinkService(linkRepository);
        }

        [Fact]
        public async Task Delete_ExistingLink_Should_Delete_It_AndShould_VerifyRelatedDependencies()
        {
            var result = await _linkService.DeleteLink(_shortId);
            result.Should().Be(true);
            _linksRepositoryMock.Verify(r => r.FirstOrDefaultAsync(It.IsAny<GetLinkByShortIdSpec>()), Times.Once);
            _linksRepositoryMock.Verify(r => r.DeleteAsync(It.Is<Link>(link => Assertion.Match(link, _link, new[] { "Utc" }))), Times.Once());
        }
    }
}
