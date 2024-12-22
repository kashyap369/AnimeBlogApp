using Anime.web.Data;
using Anime.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Anime.web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AnimeDbContext animeDbContext;
        public BlogPostRepository(AnimeDbContext animeDbContext)
        {
            this.animeDbContext = animeDbContext;
        }


        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await animeDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await animeDbContext.BlogPosts.AddAsync(post);
            await animeDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existblog = await animeDbContext.BlogPosts.FindAsync(id);
            if (existblog != null)
            {
                animeDbContext.BlogPosts.Remove(existblog);
                await animeDbContext.SaveChangesAsync();
                return existblog;
            }
            return null;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await animeDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost post)
        {
            var existBlog = await animeDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==post.Id);    
            if (existBlog != null)
            {
                existBlog.Id = post.Id;
                existBlog.Heading = post.Heading;
                existBlog.PageTitle = post.PageTitle;
                existBlog.Content = post.Content;
                existBlog.ShortDescription = post.ShortDescription;
                existBlog.FeaturedImgUrl = post.FeaturedImgUrl;
                existBlog.UrlHandle = post.UrlHandle;
                existBlog.PublishedDate = post.PublishedDate;
                existBlog.Author = post.Author;
                existBlog.Visible = post.Visible;
                existBlog.Tags = post.Tags; 

                await animeDbContext.SaveChangesAsync();
                return existBlog;

            }
            return null;

        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await animeDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.UrlHandle==urlHandle);   
        }
    }
}
