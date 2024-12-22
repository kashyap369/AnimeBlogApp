using Anime.web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Anime.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository; 
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync( IFormFile file)
        {
            var imageurl = await imageRepository.UploadAsync(file);
            if(imageurl == null)
            { 
                return Problem("something went wrong ", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageurl });
                
        }
    }
}
