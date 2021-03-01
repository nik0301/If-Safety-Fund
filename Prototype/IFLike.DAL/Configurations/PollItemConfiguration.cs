using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFLike.DAL.Configurations
{
    class PollItemConfiguration : IEntityTypeConfiguration<PollItem>
    {
        public void Configure(EntityTypeBuilder<PollItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(255);
        }
    }
}
