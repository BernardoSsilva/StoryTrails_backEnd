using Microsoft.EntityFrameworkCore;
using StoryTrails.Domain.Entities;

namespace StoryTrails.Domain.Infra
{
    public class DatabaseSettings:DbContext
    {
        public DatabaseSettings(DbContextOptions<DatabaseSettings> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
