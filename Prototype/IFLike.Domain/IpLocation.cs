using System;
using System.Collections.Generic;
using System.Text;

namespace IFLike.Domain
{
   public class IpLocation: EntityBaseId<long>
    {
        public long IpFrom { get; set; }
        public long IpTo { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
    }
}
