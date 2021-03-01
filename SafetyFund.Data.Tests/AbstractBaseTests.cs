using Microsoft.EntityFrameworkCore;

namespace SafetyFund.Data.Tests
{
    public class AbstractBaseTests
    {
        protected DbContextOptions GetDbOptions()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer("Server=swb32051\\db_st01;Database=JuniorSchool_SafetyFund_AT;User ID=JuniorSchool_admin;Password=B8z5QaAMDe;MultipleActiveResultSets=true");
            return builder.Options;
        }
    }
}
