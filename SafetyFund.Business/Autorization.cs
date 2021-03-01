using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;

namespace SafetyFund.Business
{
    public class Autorization
    {
        private readonly UserRepo repo;


        public Autorization(UserRepo repo)
        {
            this.repo = repo;
        }


        public bool IsAuthorized(string id)
        {
            if (repo.Get(id) != null)
            {
                return true;
            }

            if (repo.GetUserCount() != 0)
            {
                return false;
            }

            repo.Add(new User
            {
                Id = id,
                FullName = id
            });

            return true;
        }
    }
}
