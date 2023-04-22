using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.Model;

namespace SE_PoliceInspectorate.Controllers
{
    [Authorize]
    public class CriminalFilesController : Controller
    {
        private readonly ICriminalFilesRepository _criminalFilesRepository;

        public CriminalFilesController(ICriminalFilesRepository criminalFilesRepository)
        {
            _criminalFilesRepository = (ICriminalFilesRepository?)criminalFilesRepository;
        }

        // GET: CriminalFiles
        public async Task<IActionResult> Index(string searchString = null)
        {
            var files = _criminalFilesRepository.Search(searchString);
            return View(await files.ToListAsync());
        }

        // GET: CriminalFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criminal = await _criminalFilesRepository.GetByIdAsync(id.GetValueOrDefault());

            if (criminal == null)
            {
                return NotFound();
            }

            return View(criminal);
        }

        // GET: CriminalFiles/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName");
            ViewData["UpdatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName");
            return View();
        }

        // POST: CriminalFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NationalIdNumber,Description,Alias,Name,Address,Phone,Email,Felony,Sentence,IncarcerationDate,ExpectedReleaseDate")] Criminal criminal)
        {
            if (ModelState.IsValid)
            {

                criminal.CreatedById = _criminalFilesRepository.GetCurrentUserId();
                criminal.UpdatedById = _criminalFilesRepository.GetCurrentUserId();
                criminal.CreatedAt = DateTime.Now;
                criminal.UpdatedAt = DateTime.Now;
                _criminalFilesRepository.Add(criminal);
                await _criminalFilesRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName", criminal.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName", criminal.UpdatedById);
            return View(criminal);
        }

        // GET: ClassifiedFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criminal = await _criminalFilesRepository.GetByIdAsync(id.GetValueOrDefault());
            if (criminal == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName", criminal.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName", criminal.UpdatedById);
            return View(criminal);
        }

        // POST: ClassifiedFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,InmateName,Felony,Sentence,IncarcerationDate,ExpectedReleaseDate")] Criminal criminal)
        {
            if (id != criminal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalFile = await _criminalFilesRepository.GetByIdAsync(id);
                    criminal.CreatedById = originalFile.CreatedById;
                    criminal.CreatedAt = originalFile.CreatedAt;
                    criminal.UpdatedById = _criminalFilesRepository.GetCurrentUserId();
                    criminal.UpdatedAt = DateTime.Now;
                    _criminalFilesRepository.Update(criminal);
                    await _criminalFilesRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriminalFileExists(criminal.Id))
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
            ViewData["CreatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName", criminal.CreatedById);
            ViewData["UpdatedById"] = new SelectList(_criminalFilesRepository.GetUsers(), "Id", "UserName", criminal.UpdatedById);
            return View(criminal);
        }

        // GET: CriminalFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classifiedFile = await _criminalFilesRepository.GetByIdAsync(id.GetValueOrDefault());
            if (classifiedFile == null)
            {
                return NotFound();
            }

            return View(classifiedFile);
        }

        // POST: CriminalFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _criminalFilesRepository.Delete(id);
            await _criminalFilesRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriminalFileExists(int id)
        {
            return _criminalFilesRepository.GetAll().Any(file => file.Id == id);
        }
    }
}
