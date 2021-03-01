using IFLike.DAL.Interfaces;
using IFLike.Services.Interfaces;
using System.Net;

namespace IFLike.Services.Implementation
{
    public class CountryValidationService : ICountryValidationService
    {
        private readonly IIpLocationRepository _ipLocationRepository;
        private readonly ICountryRepository _countryRepository;

        public CountryValidationService(IIpLocationRepository ipLocationRepository, ICountryRepository countryRepository)
        {
            _ipLocationRepository = ipLocationRepository;
            _countryRepository = countryRepository;
        }

        public CountryValidationResult IsValidForVote(IPAddress ipAdrress)
        {
            // remove when ipLocations will contain data
#if DEBUG
            return new CountryValidationResult()
            {
                CountryCode = "LV",
                Success = true
            };
#endif

#if DEBUG
            ipAdrress = IPAddress.Parse("195.13.217.249");
#endif

            var result = new CountryValidationResult();

            var ipLocation = _ipLocationRepository.GetByIp(ipAdrress);
            if (ipLocation == null && ipAdrress.ToString() == "::1")
            {
                result.Success = true;
                return result;
            }

            var countryCode = ipLocation.CountryCode.ToLower();
            var country = _countryRepository.GetByCode(countryCode);

            result.CountryCode = countryCode;
            result.Success = country != null && country.IsAllowed;

            return result;
        }
    }
}
