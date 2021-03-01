using System;
using System.Collections.Generic;
using System.Text;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFLike.DAL.Configurations
{
    class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.CountryCode).HasMaxLength(2).IsRequired();
        }
    }
}
