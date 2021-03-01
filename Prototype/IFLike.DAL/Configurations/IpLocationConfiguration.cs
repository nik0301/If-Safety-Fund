using System;
using System.Collections.Generic;
using System.Text;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFLike.DAL.Configurations
{
    class IpLocationConfiguration : IEntityTypeConfiguration<IpLocation>
    {
        public void Configure(EntityTypeBuilder<IpLocation> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IpFrom).IsRequired();
            builder.Property(e => e.IpTo).IsRequired();
            builder.Property(e => e.CountryCode).HasMaxLength(2).IsRequired();
            builder.Property(e => e.CountryName).HasMaxLength(64).IsRequired();
            builder.Property(e => e.RegionName).HasMaxLength(128).IsRequired();
            builder.Property(e => e.CityName).HasMaxLength(128).IsRequired();
        }
    }
}
