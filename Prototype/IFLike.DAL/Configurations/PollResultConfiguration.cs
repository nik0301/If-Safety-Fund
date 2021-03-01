using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFLike.DAL.Configurations
{
    class PollResultConfiguration : IEntityTypeConfiguration<PollResult>
    {
        public void Configure(EntityTypeBuilder<PollResult> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Created).IsRequired();
            builder.Property(e => e.CountryCode).HasMaxLength(5);
            builder.Property(e => e.UserEmail).HasMaxLength(255);
        }
    }
}
