using Anime.web.Models.Domain;

namespace Anime.web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid BlogPostId);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);   


    }
}
