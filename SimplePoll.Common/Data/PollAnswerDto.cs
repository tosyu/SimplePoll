namespace SimplePoll.Common.Data
{
    public class PollAnswerDto
    {
        public int Id { get; set; }
        public required PollElementConfigurationDto PollElement { get; set; }
        public required string Value { get; set; }
    }
}
