using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models.Configurations;

namespace SimplePoll.CoreServer.Data.Models
{
    [EntityTypeConfiguration(typeof(PollElementConfiguration))]
    public class PollElement
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public PollElementType Type { get; set; }
        public PollElementSubtype? Subtype { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

        public ICollection<PollElementValue>? Values { get; set; }

        public ICollection<PollAnswer>? Answers { get; set; }

        public required int PollId { get; set; }
        public required Poll Poll { get; set; }
    }
}
