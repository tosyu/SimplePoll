using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models;
using SimplePoll.CoreServer.Exceptions;

namespace SimplePoll.CoreServer.Data.Repositories
{
    public class PollRepository : IPollRepository, IDisposable
    {
        private readonly SimplePollDbContext context;
        private bool disposed;

        public PollRepository(SimplePollDbContext context)
        {
            this.context = context;
        }
        public void Create(Poll poll)
        {
            context.Polls.Add(poll);
        }

        public void Delete(Poll poll)
        {
            this.Delete(poll.Id);
        }

        public void Delete(int id)
        {
            var found = context.Polls.Find(id);
            if (found == null)
            {
                throw new NotFoundException();
            }

            context.Polls.Remove(found);

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Poll>> GetPollsAsync()
        {
            return await context
                .Polls
                .ToListAsync();
        }

        public async Task<Poll> GetPollAsync(int id)
        {
            return await context
                .Polls
                .Include(i => i.Elements)
                .ThenInclude(i => i.Values)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Poll poll)
        {
            context.Entry(poll).State = EntityState.Modified;
        }
    }
}
