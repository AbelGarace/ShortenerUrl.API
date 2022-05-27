using FluentAssertions;
using Moq;
using ShortenerUrl.Api.ApplicationCore.Entities;
using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShortenerUrl.Api.UnitTest.LinkServiceTests
{
    public class LinkService_CreateShortLinkByUrl_Test
    {
        private readonly Mock<IRepository<Link>> _linksRepositoryMock;
        private readonly LinkService _linkService;
        private readonly string _url;
        private readonly Link _link;
        private readonly Link _expectedResult;

        public LinkService_CreateShortLinkByUrl_Test()
        {
            _url = "https://wwww.google.es";

            _link = new Link
            {
                Id = Guid.Empty,
                Url = "https://wwww.google.es",
                ShortId = "Q6e7u7",
                RequestId = 2,
                Clicks = 0
            };

            _expectedResult = new Link
            {
                Id = Guid.Empty,
                Url = "https://wwww.google.es",
                ShortId = "Q6e7u7",
                RequestId = 2,
                Clicks = 0,
                LastAccessDate = DateTime.Now.Date,
            };

            _linksRepositoryMock = new Mock<IRepository<Link>>();
            _linksRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ISpecification<Link>>()))
                .ReturnsAsync(_link);

            _linksRepositoryMock
               .Setup(r => r.AddAsync(It.IsAny<Link>()))
               .ReturnsAsync(_expectedResult);

            _linksRepositoryMock
               .Setup(r => r.ListAllAsync())
               .ReturnsAsync(new List<Link> { _link });

            var linkRepository = _linksRepositoryMock.Object;

            _linkService = new LinkService(linkRepository);


        }

        [Fact]
        public async Task Delete_ExistingLink_Should_Delete_It_AndShould_VerifyRelatedDependencies()
        {
            var result = await _linkService.CreateShortLink(_url);
            result.Should().BeEquivalentTo(_expectedResult);
        }
    }
}
