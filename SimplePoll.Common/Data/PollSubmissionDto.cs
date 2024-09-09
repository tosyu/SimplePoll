namespace SimplePoll.Common.Data
{
    public class PollSubmissionDto
    {
        public int PollId { get; set; }
        public required string UserName { get; set; }
        public required ICollection<PollSubmissionAnswerDto> Answers { get; set; }
    }
}
