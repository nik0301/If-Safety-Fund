using System;
using System.Collections.Generic;
using System.Text;
using IFLike.DAL.Configurations;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IFLike.DAL.Context
{
    public class IFLikeContext : IdentityDbContext<ApplicationUser>
    {
        public IFLikeContext()
        {

        }

        public IFLikeContext(DbContextOptions<IFLikeContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollItem> PollItems { get; set; }
        public DbSet<PollResult> PollResults { get; set; }
        public DbSet<IpLocation> IpLocations { get; set; }


      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new PollConfiguration());
            modelBuilder.ApplyConfiguration(new PollItemConfiguration());
            modelBuilder.ApplyConfiguration(new PollResultConfiguration());
            modelBuilder.ApplyConfiguration(new IpLocationConfiguration());

            modelBuilder.Entity<PollItem>()
                .HasOne(p => p.Poll)
                .WithMany(p => p.PollItems);
            modelBuilder.Entity<PollItem>()
                .HasMany(p => p.Images)
                .WithOne(p => p.PollItem);
            modelBuilder.Entity<PollResult>().HasOne(p => p.PollItem);
            base.OnModelCreating(modelBuilder);
        }
    }
}
