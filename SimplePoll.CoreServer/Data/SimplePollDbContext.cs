using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models;

namespace SimplePoll.CoreServer.Data
{
    public class SimplePollDbContext : DbContext
    {
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollElement> Elements { get; set; }
        public DbSet<PollElementValue> Values { get; set; }
        public DbSet<PollSubmission> Submissions { get; set; }
        public DbSet<PollAnswer> Answers { get; set; }

        public SimplePollDbContext(DbContextOptions<SimplePollDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Polls");
        }
    }
}
