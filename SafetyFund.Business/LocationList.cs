using System.Collections.Generic;
using SafetyFund.Data.Models;
using SafetyFund.Data.Repositories;

namespace SafetyFund.Business
{
    public class LocationList
    {
        private readonly LocationRepo repo;

        public LocationList(LocationRepo repo)
        {
            this.repo = repo;
        }

        public IList<Location> Get()
        {
            return repo.Get();
        }


        public void DeleteById(int id)
        {
            repo.Delete(repo.Get(id));
        }

        public virtual Location Get(int id)
        {
            return repo.Get(id);
        }

        public void Update(Location location)
        {
            repo.Update(location);
        }

        public void Add(Location location)
        {
            repo.Add(location);
        }
    }
}
