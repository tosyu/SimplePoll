using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePoll.CoreServer.Data.Models.Configurations
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(1024);

            builder
                .HasMany(b => b.Elements)
                .WithOne(b => b.Poll)
                .HasForeignKey(b => b.PollId)
                .HasPrincipalKey(b => b.Id)
                .IsRequired();

            builder
                .HasMany(b => b.Submissions)
                .WithOne(b => b.Poll)
                .HasForeignKey(b => b.PollId)
                .HasPrincipalKey(b => b.Id)
                .IsRequired();
        }
    }
}
