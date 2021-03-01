using System.Collections.Generic;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;

namespace SafetyFund.Business
{
    public class UserList
    {
        private readonly UserRepo repo;


        public UserList(UserRepo repo)
        {
            this.repo = repo;
        }


        public virtual IList<User> Get()
        {
            return repo.Get();
        }


        public void UpdateUser(User user, bool isExisting)
        {
            if (isExisting && repo.Get(user.Id) != null)
            {
                repo.Update(user);
            }
            else if (!isExisting)
            {
                if (repo.Get(user.Id) != null)
                {
                    throw new UserAlreadyExistsException();
                }

                repo.Add(user);
            }
        }


        public void DeleteById(string id)
        {
            repo.Delete(repo.Get(id));
        }


        public virtual User Get(string id)
        {
            return repo.Get(id);
        }
    }
}
