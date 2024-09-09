using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models.Configurations;

namespace SimplePoll.CoreServer.Data.Models
{
    [EntityTypeConfiguration(typeof(PollElementValueConfiguration))]
    public class PollElementValue
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required int PollElementId { get; set; }
        public required PollElement PollElement { get; set; }
    }
}
