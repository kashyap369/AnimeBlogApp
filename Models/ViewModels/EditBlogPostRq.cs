using Microsoft.AspNetCore.Mvc.Rendering;

namespace Anime.web.Models.ViewModels
{
    public class EditBlogPostRq
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImgUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // display tags 
        public IEnumerable<SelectListItem> Tags { get; set; }
        // collect tags 
        public string[] SelectedTag { get; set; } = Array.Empty<string>();
    }
}
