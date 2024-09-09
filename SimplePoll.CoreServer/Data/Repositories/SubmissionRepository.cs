using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data.Models;
using SimplePoll.CoreServer.Exceptions;

namespace SimplePoll.CoreServer.Data.Repositories
{
    public class SubmissionRepository : ISubmissionRepository, IDisposable
    {
        private readonly SimplePollDbContext context;

        public bool disposed { get; private set; }

        public SubmissionRepository(SimplePollDbContext context)
        {
            this.context = context;
        }

        public void Create(PollSubmission submission)
        {
            submission.CreatedAt = DateTime.UtcNow;

            context.Submissions.Add(submission);
        }

        public void Delete(PollSubmission submission)
        {
            this.Delete(submission.Id);
        }

        public void Delete(int id)
        {
            var found = context.Submissions.Find(id);
            if (found == null)
            {
                throw new NotFoundException();
            }

            context.Submissions.Remove(found);
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

        public async Task<IEnumerable<PollSubmission>> GetSubmissions()
        {
            return await context.Submissions.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(PollSubmission submission)
        {
            context.Entry(submission).State = EntityState.Modified;
        }
    }
}
