using System;
using System.Collections.Generic;
using System.Text;
using IFLike.Domain;

namespace IFLike.DAL.Interfaces
{
    public interface IImageRepository : IRepositoryBase<Image, int>
    {
        List<Image> GetByPollItemId(int argId);
    }
}
