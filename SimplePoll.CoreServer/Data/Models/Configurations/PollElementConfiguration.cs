using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePoll.CoreServer.Data.Models.Configurations
{
    public class PollElementConfiguration : IEntityTypeConfiguration<PollElement>
    {
        public void Configure(EntityTypeBuilder<PollElement> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(2048);


            builder
                .HasMany(b => b.Values)
                .WithOne(b => b.PollElement)
                .HasForeignKey(b => b.PollElementId)
                .HasPrincipalKey(b => b.Id)
                .IsRequired();

            builder
                .HasMany(b => b.Answers)
                .WithOne(b => b.PollElement)
                .HasForeignKey(b => b.PollElementId)
                .HasPrincipalKey(b => b.Id)
                .IsRequired();
        }
    }
}
