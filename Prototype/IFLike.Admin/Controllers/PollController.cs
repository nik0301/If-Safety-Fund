using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IFLike.DAL.Context;
using IFLike.DAL.Interfaces;
using IFLike.Domain;
using IFLike.Dto;
using IFLike.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IFLike.Admin.Controllers
{
    public class PollController : BaseController
    {

        private IPollRepository _pollRepository;
        private IMapper _mapper;
        private IFLikeContext _context;

        public PollController(
            IPollRepository pollRepository,
            IMapper mapper,
            IFLikeContext context
            )
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _pollRepository.All();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new PollCreateDto();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PollCreateDto model, IFormFile file)
        {
            var poll = new Poll();
            _mapper.Map(model.Poll, poll);
            poll.Created = DateTime.Now;
            var pollitem = new PollItem();
            _mapper.Map(model.Item, pollitem);

            poll.PollItems.Add(pollitem);

            var pollImage = new Image();
            if (file != null)
            {
                pollImage.FileName = Path.GetFileName(file.FileName);
                pollImage.Content = ImageHelper.ReadImage(file.OpenReadStream());
                pollitem.Images.Add(pollImage);
            }


            _context.Add(poll);
            _context.Add(pollitem);
            if (pollImage.Content != null)
            {
                pollitem.Images.Add(pollImage);
                _context.Add(pollImage);

            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var model = _pollRepository.GetById(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PollDto model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            return RedirectToAction("Index");
        }
    }
}