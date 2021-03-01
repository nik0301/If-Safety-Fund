using Microsoft.EntityFrameworkCore;
using SafetyFund.Data.Models;

namespace SafetyFund.Data
{
    public class SafetyFundDbContext : DbContext
    {
        public SafetyFundDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
