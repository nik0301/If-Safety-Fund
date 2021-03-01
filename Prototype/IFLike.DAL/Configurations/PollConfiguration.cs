using System;
using System.Collections.Generic;
using System.Text;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFLike.DAL.Configurations
{
    class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.End).IsRequired();
            builder.Property(e => e.Start).IsRequired();
            builder.Property(e => e.Created).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(255);
        }
    }
}
