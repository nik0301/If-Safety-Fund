using IFLike.DAL.Context;
using IFLike.DAL.Implementation;
using IFLike.Domain;
using System.Linq;

namespace IFLike.DAL.Interfaces
{
    public class CountryRepository : RepositoryBase<Country, int>, ICountryRepository
    {
        public CountryRepository(IFLikeContext context)
            :base(context)
        {
        }

        public Country GetByCode(string countryCode)
        {
            return Context.Countries.FirstOrDefault(c => c.CountryCode == countryCode);
        }
    }
}
