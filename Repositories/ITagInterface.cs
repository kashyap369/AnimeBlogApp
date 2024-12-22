using Anime.web.Models.Domain;

namespace Anime.web.Repositories
{
    public interface ITagInterface
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);    
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag> DeleteAsync(Guid id);


    }
}
