using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess;
using SE_PoliceInspectorate.DataAccess.Model;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace PoliceInspectorate.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _userRepository;

        public UsersController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = _userRepository.GetAll();
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["PoliceStationId"] = new SelectList(_userRepository.GetStations(), "Id", "Title");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,Password,FirstName,LastName,PoliceStationId")] User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Add(user);
                await _userRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoliceStationId"] = new SelectList(_userRepository.GetStations(), "Id", "Title", user.PoliceStationId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(id.GetValueOrDefault());
            if (user == null)
            {
                return NotFound();
            }
            ViewData["PoliceStationId"] = new SelectList(_userRepository.GetStations(), "Id", "Title", user.PoliceStationId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Email,FirstName,LastName,PoliceStationId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.Update(user);
                    await _userRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoliceStationId"] = new SelectList(_userRepository.GetStations(), "Id", "Title", user.PoliceStationId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(id.GetValueOrDefault());
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userRepository.Delete(id);
            await _userRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _userRepository.GetAll().Any(e => e.Id == id);
        }
    }

}