using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using IFLike.Domain;

namespace IFLike.DAL.Interfaces
{
    public interface IIpLocationRepository
    {
        IpLocation GetByIp(IPAddress ip);

    }
}
