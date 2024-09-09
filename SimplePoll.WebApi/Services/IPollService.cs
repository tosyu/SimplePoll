using SimplePoll.Common.Data;

namespace SimplePoll.WebApi.Services
{
    public interface IPollService
    {
        public Task<IEnumerable<PollConfigurationDto>?> GetPollsAsync();
        public Task<PollConfigurationDto?> GetPollAsync(int id);
        public Task<PollSubmissionResultDto?> Submit(int id, PollSubmissionRequestDto submission);
    }
}
