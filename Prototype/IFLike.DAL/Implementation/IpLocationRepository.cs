using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using IFLike.DAL.Context;
using IFLike.DAL.Interfaces;
using IFLike.Domain;
using Microsoft.EntityFrameworkCore;

namespace IFLike.DAL.Implementation
{
    public class IpLocationRepository : IIpLocationRepository
    {

        protected IFLikeContext Context;

        public IpLocationRepository(IFLikeContext iFLikeContext)
        {
            Context = iFLikeContext;
        }
        public IpLocation GetByIp(IPAddress ip)
        {
            byte[] bytes = ip.GetAddressBytes();
            Array.Reverse(bytes);
            var ipAsLong = (long)BitConverter.ToUInt32(bytes, 0);

            return Context.IpLocations.FromSql("EXECUTE [dbo].[GetIpLocation] {0}", ipAsLong).FirstOrDefault();
        }
    }
}
