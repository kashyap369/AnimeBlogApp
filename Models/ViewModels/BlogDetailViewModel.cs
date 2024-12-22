using Anime.web.Models.Domain;

namespace Anime.web.Models.ViewModels
{
    public class BlogDetailViewModel
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

        //navigation property
        public ICollection<Tag> Tags { get; set; }

        public int TotalLikes { get; set; }
    }
}
