using Microsoft.EntityFrameworkCore;
using StoryTrails.Domain.Entities;

namespace StoryTrails.Domain.Infra
{
    public class DatabaseSettings:DbContext
    {
        public DatabaseSettings(DbContextOptions<DatabaseSettings> options):base(options)
        {
            
        }

        public DbSet<User> users { get; set; }
        public DbSet<Collection> collections { get; set; }

        public DbSet<Book> books { get; set; }
    }
}
