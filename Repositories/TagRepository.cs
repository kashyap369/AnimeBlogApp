using Anime.web.Data;
using Anime.web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Anime.web.Repositories
{
    public class TagRepository : ITagInterface  
    {
        private readonly AnimeDbContext _context;
        public TagRepository(AnimeDbContext dbcontext)
        {
            _context = dbcontext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;

        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
            var exist = await _context.Tags.FindAsync(id);
            if (exist != null)
            {
                    _context.Tags.Remove(exist);    
                await _context.SaveChangesAsync();
                return exist;
            }
            return null;

        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
            
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);    
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var exist = await _context.Tags.FindAsync(tag.Id);

            if (exist != null)
            {
                exist.Name = tag.Name;
                exist.DisplayName = tag.DisplayName;
                await _context.SaveChangesAsync();      
                return exist;
            }
            return null;        


        }
    }
}
