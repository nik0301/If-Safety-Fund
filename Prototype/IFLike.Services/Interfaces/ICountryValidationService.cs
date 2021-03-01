using System.Net;

namespace IFLike.Services.Interfaces
{
    public interface ICountryValidationService : IServiceBase
    {
        CountryValidationResult IsValidForVote(IPAddress ipAdrress);
    }
}
