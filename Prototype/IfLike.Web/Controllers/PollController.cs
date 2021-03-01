using Microsoft.AspNetCore.Mvc;
using IFLike.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using IfLike.Web.SignalR;

namespace IfLike.Web.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollService _pollService;
        private readonly ICountryValidationService _countryValidationService;
        private readonly IPollResultService _pollResultService;
        private readonly IPollItemService _pollItemService;

        private readonly IHubContext<VoteHub> _rulesHubContext;
        
        public PollController(
            IPollService pollService,
            ICountryValidationService countryValidationService,
            IPollResultService pollResultService,
            IPollItemService pollItemService,
            IHubContext<VoteHub> rulesHubContext)
        {
            _pollService = pollService;
            _countryValidationService = countryValidationService;
            _pollResultService = pollResultService;
            _pollItemService = pollItemService;
            _rulesHubContext = rulesHubContext;
        }

        [HttpGet]
        public IActionResult Q(int id)
        {
            var result = _pollService.GetById(id);
            if (TempData.ContainsKey("Message"))
            {
                result.Message = (string)TempData["Message"];
            }
            return View(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Vote(int id)
        {
            var message = "Thank you for your vote!";
            var ip = Request.HttpContext.Connection.RemoteIpAddress;
            var validationResult = _countryValidationService.IsValidForVote(ip);

            if (validationResult.Success)
            {
                bool voteConfirmed = _pollResultService.AddVote(HttpContext.User.Identity.Name, id, validationResult.CountryCode);

                if (!voteConfirmed)
                {
                    message = "Sorry you can`t vote again";
                } else
                {
                    // sending new vote count to clients
                    var voteCount = _pollResultService.GetVoteCount(id);
                    _rulesHubContext.Clients.All.InvokeAsync("UpdateVoteCount", new { id = id, count = voteCount });
                }
            }
            else
            {
                message = "Unfortunately your country can not participate in the voting.";
            }

            TempData["Message"] = message;

            var pollId = _pollItemService.Find(id)?.PollId;

            return RedirectToAction("q", new { id = pollId });
        }
    }
}