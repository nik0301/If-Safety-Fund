using System.Linq;
using Microsoft.EntityFrameworkCore;
using SafetyFund.Data.Models;

namespace SafetyFund.Data.Repositories
{
    public class LocationRepo:AbstractRepo<Location,int>
    {
        public LocationRepo(DbContextOptions options) : base(options)
        {
        }


        public virtual bool IsCountryAllowed(string country)
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                return db.Locations.Any(item => item.Country == country);
            }
        }
    }
}
