using Moq;
using ShortenerUrl.Api.ApplicationCore.Dtos;
using ShortenerUrl.Api.ApplicationCore.Entities;
using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShorternerUrl.Api.Services;
using System;
using System.Threading.Tasks;
using Xunit;


namespace ShortenerUrl.Api.UnitTests.LinkDtoServiceTests
{
    public class LinkDtoService_GetByShortId_Test
    {
        private readonly Mock<ILinkService> _linkServiceMock;
        private readonly Mock<ILinkAssembler> _linkAssemblerMock;
        private readonly LinkDtoService _linkDtoService;
        private readonly LinkDto _expectedResult;
        private readonly string _shortId;
        private readonly string _baseUrl;

        public LinkDtoService_GetByShortId_Test()
        {
            _shortId = "Q6e7uu";
            _baseUrl = "https://localhost/";
            _linkServiceMock = new Mock<ILinkService>();
            _linkAssemblerMock = new Mock<ILinkAssembler>();

            var link = new Link
            {
                Id = Guid.NewGuid(),
                Url = "https://wwww.google.es",
                ShortId = "Q6e7uu",
                LastAccessDate = DateTime.Now,
                RequestId = 1,
                Clicks = 0
            };

            _linkServiceMock
                .Setup(l => l.GetByShortId(_shortId))
                .ReturnsAsync(link);

            var linkService = _linkServiceMock.Object;

            var linkDto = new LinkDto
            {
                Url = "https://wwww.google.es",
                ShortId = "Q6e7uu",
                Clicks = 0,
                ShortUrl = $"{_baseUrl}/Q6e7uu"
            };

            _linkAssemblerMock
                .Setup(l => l.GetLinkDto(link))
                .Returns(linkDto);

            var linkAssembler = _linkAssemblerMock.Object;

            _linkDtoService = new LinkDtoService(linkService, linkAssembler);

            _expectedResult = new LinkDto
            {
                Url = "https://wwww.google.es",
                ShortId = "Q6e7uu",
                Clicks = 0,
                ShortUrl = $"{_baseUrl}/Q6e7uu"
            };

        }

        [Fact]
        public async Task Given_LinkDto_Should_ReturnExpectedResult()
        {
            var Url = "https://wwww.google.es";
            var ShortId = "Q6e7uu";
            var Clicks = 0;
            var ShortUrl = $"{_baseUrl}/Q6e7uu";

            var linkDto = await _linkDtoService.GetByShortId(_shortId);
            //result.Should().BeEquivalentTo(_expectedResult);

            // Assert
            Assert.NotNull(linkDto);
            Assert.Equal(Url, linkDto.Url);
            Assert.Equal(Clicks, linkDto.Clicks);
            Assert.Equal(ShortUrl, linkDto.ShortUrl);
            Assert.Equal(ShortId, linkDto.ShortId);
        }
    }
}
