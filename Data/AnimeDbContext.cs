using Anime.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Anime.web.Data
{
    public class AnimeDbContext : DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options):base(options) 
        {
            
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }    
        public DbSet<BlogPostLike> BlogPostLike { get; set; }




    }
}
