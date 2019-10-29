using Microsoft.EntityFrameworkCore;

namespace ListChallengeApi.Models
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) {}
        public DbSet<Root> Root { get; set; }
        public DbSet<Factory> Factory { get; set; }
        public DbSet<Child> Child { get; set; }
    }
}