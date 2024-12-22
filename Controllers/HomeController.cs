using Anime.web.Models;
using Anime.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Anime.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        public HomeController(ILogger<HomeController> logger,IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await blogPostRepository.GetAllAsync();
            return View(blogs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
