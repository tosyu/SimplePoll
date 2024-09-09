using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models.Configurations;

namespace SimplePoll.CoreServer.Data.Models
{
    [EntityTypeConfiguration(typeof(PollAnswerConfiguration))]
    public class PollAnswer
    {
        public int Id { get; set; }
        public int PollElementId { get; set; }
        public PollElement? PollElement { get; set; }
        public int PollSubmissionId { get; set; }
        public PollSubmission? PollSubmission { get; set; }
        public string? Value { get; set; }
    }
}
