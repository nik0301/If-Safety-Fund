using System;
using System.Collections.Generic;
using System.Text;
using IFLike.DAL.Interfaces;
using IFLike.Domain;
using IFLike.Services.Interfaces;

namespace IFLike.Services.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _pollImageRepository;

        public ImageService(IImageRepository pollImageRepository)
        {
            _pollImageRepository = pollImageRepository;
        }

        public Image Find(int id)
        {
            return _pollImageRepository.GetById(id);
        }

    }
}
