namespace SimplePoll.Common.Data
{
    public class PollElementConfigurationDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public string? Subtype { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

        public ICollection<PollElementValueConfigurationDto> Values { get; set; } = new List<PollElementValueConfigurationDto>();
    }
}
