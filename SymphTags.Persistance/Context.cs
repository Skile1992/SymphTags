using Microsoft.EntityFrameworkCore;
using SymphTags.Domain.Entities;

namespace SymphTags.Persistance
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        
        public DbSet<User> User { get; set; }
        public DbSet<Link> Link { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<LinkTags> LinkTags { get; set; }
    }
}
