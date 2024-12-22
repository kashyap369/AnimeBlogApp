using Anime.web.Models.ViewModels;
using Anime.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Anime.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        public BlogsController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository BlogPostLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
            blogPostLikeRepository = BlogPostLikeRepository;
        }

        public IBlogPostLikeRepository BlogPostLikeRepository { get; }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogpost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogdetailviewmodel = new BlogDetailViewModel();

            if (blogpost != null)
            {
                var TotalLikes = await blogPostLikeRepository.GetTotalLikes(blogpost.Id);

                blogdetailviewmodel = new BlogDetailViewModel
                {
                    Id = blogpost.Id,
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    ShortDescription = blogpost.ShortDescription,
                    FeaturedImgUrl = blogpost.FeaturedImgUrl,
                    UrlHandle = blogpost.UrlHandle,
                    PublishedDate = blogpost.PublishedDate,
                    Author = blogpost.Author,
                    Tags = blogpost.Tags,
                    TotalLikes = TotalLikes
                };


            }



            return View(blogdetailviewmodel);
        }
    }
}
