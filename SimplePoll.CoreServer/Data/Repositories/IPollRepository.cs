using SimplePoll.CoreServer.Data.Models;

namespace SimplePoll.CoreServer.Data.Repositories
{
    public interface IPollRepository
    {
        Task<IEnumerable<Poll>> GetPollsAsync();
        Task<Poll> GetPollAsync(int id);
        void Create(Poll poll);
        void Delete(Poll poll);
        void Delete(int id);
        void Update(Poll poll);
        Task SaveAsync();
    }
}
