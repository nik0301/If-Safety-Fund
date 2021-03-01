using System;
using System.Collections.Generic;
using System.Text;
using IFLike.Domain;

namespace IFLike.Services.Interfaces
{
    public interface IImageService
    {
        Image Find(int id);
    }
}
