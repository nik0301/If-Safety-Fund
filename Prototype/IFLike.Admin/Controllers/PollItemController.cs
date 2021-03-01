using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IFLike.Admin.Controllers
{
    public class PollItemController : BaseController
    {
        public IActionResult Index(int IdPoll)
        {
            return View();
        }
    }
}