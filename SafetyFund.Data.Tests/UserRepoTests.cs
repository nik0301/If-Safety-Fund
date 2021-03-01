using System;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;
using Xunit;

namespace SafetyFund.Data.Tests
{
    public class UserRepoTests : AbstractBaseTests
    {
        private readonly UserRepo repo;


        public UserRepoTests()
        {
            repo = new UserRepo(GetDbOptions());
        }


        public User CreateUser(string uniqueId = null)
        {
            uniqueId = uniqueId ?? "uniqueId";

            if (repo.Get(uniqueId) != null)
            {
                repo.Delete(repo.Get(uniqueId));
            }

            var user = new User()
            {
                Id = uniqueId,
                FullName = "europeUsr"
            };

            repo.Add(user);

            return user;
        }


        [Fact]
        public void When_new_user_added_Then_count_is_increased_by_one()
        {
            var currentCount = repo.GetUserCount();

            CreateUser(Guid.NewGuid().ToString());

            var newCount = repo.GetUserCount();

            Assert.Equal(currentCount + 1, newCount);
        }
    }
}
