using Microsoft.EntityFrameworkCore;
using StoryTrails.API.Entities;

namespace StoryTrails.API.Infra
{
    public class DatabaseSettings:DbContext
    {
        public DatabaseSettings(DbContextOptions<DatabaseSettings> options):base(options)
        {
            
        }

        DbSet<User> users { get; set; }
        DbSet<Collection> collections { get; set; }

        DbSet<Book> books { get; set; }
    }
}
