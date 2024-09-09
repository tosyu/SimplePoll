using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models.Configurations;

namespace SimplePoll.CoreServer.Data.Models
{
    [EntityTypeConfiguration(typeof(PollSubmissionConfiguration))]
    public class PollSubmission
    {
        public int Id { get; set; }
        public required int PollId { get; set; }
        public required Poll Poll { get; set; }
        public ICollection<PollAnswer>? Answers { get; set; }
        public required string SubmissionUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
