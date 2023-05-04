using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.Model;

namespace SE_PoliceInspectorate.Controllers
{

    [Authorize]
    public class ConferenceMessagesController : Controller
    {
        private readonly IConferenceMessageRepository _conferenceMessageRepository;

        public ConferenceMessagesController(IConferenceMessageRepository conferenceMessageRepository)
        {
            _conferenceMessageRepository = conferenceMessageRepository;
        }

        // GET: ClassifiedFiles
        public async Task<IActionResult> Index()
        {
            var users = _conferenceMessageRepository.GetUsers();
            return View(await users.ToListAsync());
        }

        // GET: ClassifiedFiles/Details/5
        public async Task<IActionResult> Conference(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = _conferenceMessageRepository.GetMessages(id.Value);

            if (messages == null)
            {
                return NotFound();
            }

            ViewData["CurrentUserId"] = _conferenceMessageRepository.GetCurrentUserId();
            ViewData["ConferenceMessage"] = await messages.ToListAsync();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Conference(int id, [Bind("Content")] ConferenceMessage conferenceMessage)
        {
            if (ModelState.IsValid)
            {
                var newConferenceMessage = new ConferenceMessage
                {
                    FromId = _conferenceMessageRepository.GetCurrentUserId(),
                    ToId = id,
                    Content = conferenceMessage.Content,
                    TimeStamp = DateTime.Now
                };
                _conferenceMessageRepository.Add(newConferenceMessage);
                await _conferenceMessageRepository.SaveAsync();
                return RedirectToAction(nameof(Conference));
            }
            return View(conferenceMessage);
        }

    }
}
