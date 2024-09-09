using SimplePoll.Common.Data;

namespace SimplePoll.CoreServer.Services
{
    public interface IPollService
    {
        public Task<IEnumerable<PollConfigurationDto>> GetPollsAsync();
        public Task<PollConfigurationDto> GetPollAsync(int id);
        public Task<PollSubmissionResultDto> SubmitPollResults(PollSubmissionDto pollSubmission);
    }
}
