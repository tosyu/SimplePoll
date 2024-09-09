namespace SimplePoll.Common.Data
{
    public class PollSubmissionRequestDto
    {
        public required string UserName { get; set; }
        public required ICollection<PollSubmissionAnswerDto> Elements { get; set; }
    }
}
