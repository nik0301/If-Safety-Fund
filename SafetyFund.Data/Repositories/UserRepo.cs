using Microsoft.EntityFrameworkCore;
using SafetyFund.Data.Models;
using System.Linq;

namespace SafetyFund.Data.Repositories
{
    public class UserRepo : AbstractRepo<User, string>
    {
        public UserRepo(DbContextOptions options) : base(options)
        {
        }

        public virtual int GetUserCount()
        {
            using (var db = new SafetyFundDbContext(Options))
            {
                return db.Users.Count();
            }
        }
    }
}
