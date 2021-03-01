using IFLike.Domain;

namespace IFLike.DAL.Interfaces
{
    public interface ICountryRepository : IRepositoryBase<Country, int>
    {
        Country GetByCode(string countryCode);
    }
}
