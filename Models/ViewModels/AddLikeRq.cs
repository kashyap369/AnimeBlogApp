namespace Anime.web.Models.ViewModels
{
    public class AddLikeRq
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
