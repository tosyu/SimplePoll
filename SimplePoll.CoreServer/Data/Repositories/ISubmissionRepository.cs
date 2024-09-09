using SimplePoll.CoreServer.Data.Models;

namespace SimplePoll.CoreServer.Data.Repositories
{
    public interface ISubmissionRepository
    {
        Task<IEnumerable<PollSubmission>> GetSubmissions();
        void Create(PollSubmission submission);
        void Delete(PollSubmission submission);
        void Delete(int id);
        void Update(PollSubmission submission);
        Task SaveAsync();
    }
}
