namespace SimplePoll.Common.Data
{
    public class PollConfigurationDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<PollElementConfigurationDto> Elements { get; set; } = new List<PollElementConfigurationDto>();
    }
}
