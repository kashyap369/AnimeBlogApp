using Anime.web.Models.Domain;
using Anime.web.Models.ViewModels;
using Anime.web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anime.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRq likerq)
        {
            var model = new BlogPostLike
            {
                BlogPostId = likerq.BlogPostId,
                UserId = likerq.UserId
            };
            await   blogPostLikeRepository.AddLikeForBlog(model);
            return Ok();
        }
    }
}
