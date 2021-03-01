using System;
using System.Collections.Generic;
using System.Text;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFLike.DAL.Configurations
{
    class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Content).IsRequired();
            builder.Property(e => e.FileName).HasMaxLength(255);
        }
    }
}
