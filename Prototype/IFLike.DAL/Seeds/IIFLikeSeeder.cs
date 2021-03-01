using System;
using System.Collections.Generic;
using System.Text;
using IFLike.DAL.Context;

namespace IFLike.DAL.Seeds
{
    public interface IIFLikeSeeder
    {
        void Seed(IFLikeContext context);
    }
}
