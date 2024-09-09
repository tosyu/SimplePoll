namespace SimplePoll.Common.Data
{
    public class PollResultsDto
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public required string PollName { get; set; }
        public required string User { get; set; }
        ICollection<PollAnswerDto> Answers { get; set; } = new List<PollAnswerDto>();
    }
}
