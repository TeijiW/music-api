using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Context {
    public class DatabaseContext : DbContext {
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base (options) {

        }

        public DbSet<Music> Musics { get; set; }
        public DbSet<Author> Authors { get; set; }
    }

}