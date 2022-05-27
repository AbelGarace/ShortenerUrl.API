using Microsoft.AspNetCore.Mvc;
using ShortenerUrl.Api.ApplicationCore.Dtos.Auth;
using ShorternerUrl.Api.Interfaces;

namespace ShorternerUrl.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class RedirectController : Controller
    {
        private readonly ILinkDtoService _linkDtoService;
        public RedirectController(ILinkDtoService linkDtoService)
        {
            _linkDtoService = linkDtoService;
        }

        /// <summary>
        /// Redirects to the URL matching the shortId or returns an error if can find it
        /// </summary>
        /// <param name="shortId"></param>
        /// <returns></returns>

        [HttpGet, Route("{shortId}")]
        public async Task<IActionResult> RedirectToUrl(string shortId)
        {
            try
            {
                var linkDto = await _linkDtoService.GetUrlToRedirectByShortId(shortId);        
                
                if (linkDto == null) return NotFound();

                return new RedirectResult(linkDto.Url);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    OkResponse = new OkResponse()
                    { IsSuccess = false, Message = ex.Message }
                });
            }
        }
    }
}
