using System.Net;
using Newtonsoft.Json;
using SafetyFund.Data.Repositories;

namespace SafetyFund.Business
{
    public class LocationService
    {
        private const string COUNTRY_REQUEST_URL = "http://ip-api.com/json/";

        private readonly LocationRepo repo;

        public LocationService(LocationRepo repo)
        {
            this.repo = repo;
        }

        public virtual bool IsCountryAllowed(IPAddress ipAddress)
        {
            var location = GetClientLocation(ipAddress.ToString());


            return repo.IsCountryAllowed(location);
        }


        private static string GetClientLocation(string clientIp)
        {
            if (clientIp == "::1")
            {
                return "Server Location";
            }

            try
            {
                using (var client = new WebClient())
                {
                    var response = client.DownloadString(COUNTRY_REQUEST_URL + clientIp);
                    return JsonConvert.DeserializeObject<dynamic>(response).country.Value.ToString();
                }

            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
