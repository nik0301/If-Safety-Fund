using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFLike.DAL.Interfaces;
using IFLike.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace IfLike.Web.Controllers
{

    public class PollImageController : Controller
    {
        private IImageService _imageService;

        public PollImageController(
            IImageService imageService
            )
        {
            _imageService = imageService;
        }
        public IActionResult Index(int imageID)
        {
            var image = _imageService.Find(imageID);
            var mymetype = MimeMapping.MimeUtility.GetMimeMapping(image.FileName);
            return File(image.Content, mymetype);
        }
    }
}