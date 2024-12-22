using Anime.web.Models.Domain;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Anime.web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

        Task<BlogPost> AddAsync(BlogPost post); 

        Task<BlogPost> UpdateAsync(BlogPost post);

        Task<BlogPost?> DeleteAsync(Guid id);

    }
}
