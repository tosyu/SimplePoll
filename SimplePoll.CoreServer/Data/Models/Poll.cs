using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models.Configurations;

namespace SimplePoll.CoreServer.Data.Models
{
    [EntityTypeConfiguration(typeof(PollConfiguration))]
    public class Poll
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<PollElement>? Elements { get; set; } = new List<PollElement>();
        public ICollection<PollSubmission>? Submissions { get; set; } = new List<PollSubmission>();
    }
}
