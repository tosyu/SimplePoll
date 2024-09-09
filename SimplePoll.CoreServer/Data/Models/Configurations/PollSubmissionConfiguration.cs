using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePoll.CoreServer.Data.Models.Configurations
{
    public class PollSubmissionConfiguration : IEntityTypeConfiguration<PollSubmission>
    {
        public void Configure(EntityTypeBuilder<PollSubmission> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasMany(b => b.Answers)
                .WithOne(b => b.PollSubmission)
                .HasForeignKey(b => b.PollSubmissionId)
                .HasPrincipalKey(b => b.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
