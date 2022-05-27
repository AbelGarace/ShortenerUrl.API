using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortenerUrl.Api.ApplicationCore.Dtos.Auth;
using ShortenerUrl.Api.ApplicationCore.Dtos.Request;
using ShortenerUrl.Api.ApplicationCore.Utils;
using ShorternerUrl.Api.Interfaces;

namespace ShorternerUrl.Api.Controllers
{
    [Authorize]
    [ApiController]    
    public class ShortLinksController : Controller
    {
        private readonly ILinkDtoService _linkDtoService;
        public ShortLinksController(ILinkDtoService linkDtoService)
        {
            _linkDtoService = linkDtoService;
        }

        /// <summary>
        /// Create a short link from orignal Url
        /// </summary>
        /// <param name="request">Request object with Url</param>
        /// <returns>Dto with Link entity details</returns>
        [HttpPost, Route("CreateShortLink")]
        public async Task<IActionResult> CreateShortLink(RequestCreateShortUrl request)
        {
            try
            {
                var baseUrl = GetBaseUrl();
                var linkDto = await _linkDtoService.GetByUrl(request.Url);
                return new OkObjectResult(new
                {
                    OkResponse = new OkResponse()
                    { IsSuccess = true, Message = "Get ShortLink successfully retrieved" },
                    LinkDto = linkDto
                });
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

        /// <summary>
        /// Get Link Dto searching by ShortId
        /// </summary>
        /// <param name="shortId">ShortId to search Link</param>
        /// <returns></returns>
        [HttpGet, Route("GetLink/{shortId}")]
        public async Task<IActionResult> GetShortLink(string shortId)
        {
            try
            {
                var baseUrl = GetBaseUrl();
                var linkDto = await _linkDtoService.GetByShortId(shortId);
                return new OkObjectResult(new
                {
                    OkResponse = new OkResponse()
                    { IsSuccess = true, Message = "Get ShortLink successfully retrieved" },
                    LinkDto = linkDto
                });
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

        /// <summary>
        /// Get All available Links
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAllLinks")]
        public async Task<IActionResult> GetAllLinks()
        {
            try
            {
                var baseUrl = GetBaseUrl();
                var linkDtos = await _linkDtoService.GetAllLinks();
                return new OkObjectResult(new
                {
                    OkResponse = new OkResponse()
                    { IsSuccess = true, Message = "Get All ShortLinks successfully retrieved" },
                    LinkDtos = linkDtos
                });
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

        /// <summary>
        /// Delete Link from database
        /// </summary>
        /// <param name="request">Request object with ShortId to delete</param>
        /// <returns></returns>
        [HttpPost, Route("DeleteLink")]
        public async Task<IActionResult> DeleteLink(RequestDeleteLink request)
        {
            try
            {                
                var result = await _linkDtoService.DeleteLink(request.ShortId);

                return new OkObjectResult(new
                {
                    OkResponse = new OkResponse()
                    {
                        IsSuccess = result,
                        Message = "Links was Deleted"
                    }
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    OkResponse = new OkResponse()
                    { IsSuccess = false, Message = "There was a problem deleting link." }                    
                });
            }
        }

        /// <summary>
        /// Private method to get base url from enviroment
        /// </summary>
        /// <returns></returns>
        private string GetBaseUrl()
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}";
            UriUtils.baseUrl = baseUrl;
            return baseUrl;
        }
    }
}
