using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFLike.DAL.Context;
using IFLike.DAL.Interfaces;
using IFLike.Domain;

namespace IFLike.DAL.Implementation
{
    public  class ImageRepository : RepositoryBase<Image, int>, IImageRepository
    {
        public ImageRepository(IFLikeContext iFLikeContext) : base(iFLikeContext)
        {
        }

        public List<Image> GetByPollItemId(int argId)
        {
            return Context.Images.Where(i => i.PollItemId == argId).Select(i => new Image() {Id = i.Id, FileName = i.FileName, IsDefault = i.IsDefault})
                .ToList();
            
        }
    }
}
