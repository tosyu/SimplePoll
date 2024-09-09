using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimplePoll.CoreServer.Data.Models.Configurations
{
    public class PollElementValueConfiguration : IEntityTypeConfiguration<PollElementValue>
    {
        public void Configure(EntityTypeBuilder<PollElementValue> builder)
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
        }
    }
}
