
using Anime.web.Data;
using Anime.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Anime.web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly AnimeDbContext dbContext;

        public BlogPostLikeRepository(AnimeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
          ]
        ]

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await dbContext.BlogPostLike.AddAsync(blogPostLike);
            await dbContext.SaveChangesAsync();
            return blogPostLike;


        }

        public async Task<int> GetTotalLikes(Guid BlogPostId)
        {
            return await dbContext.BlogPostLike.CountAsync(x => x.BlogPostId == BlogPostId);    
        }
    }
}
