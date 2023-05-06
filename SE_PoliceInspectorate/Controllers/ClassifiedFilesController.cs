using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.Model;
using Microsoft.EntityFrameworkCore;


namespace SE_PoliceInspectorate.Controllers
{


        [Authorize]
        public class ClassifiedFilesController : Controller
        {
            private readonly IClassifiedFilesRepository _classifiedFilesRepository;

            public ClassifiedFilesController(IClassifiedFilesRepository classifiedFilesRepository)
            {
                _classifiedFilesRepository = classifiedFilesRepository;
            }

            // GET: ClassifiedFiles
            public async Task<IActionResult> Index(string? searchString)
            {
                var files = _classifiedFilesRepository.Search(searchString);
                return View(await files.ToListAsync());
            }

            // GET: ClassifiedFiles/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var classifiedFile = await _classifiedFilesRepository.GetByIdAsync(id.GetValueOrDefault());

                if (classifiedFile == null)
                {
                    return NotFound();
                }

                return View(classifiedFile);
            }

            // GET: ClassifiedFiles/Create
            public IActionResult Create()
            {
                ViewData["CreatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName");
                ViewData["UpdatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName");
                return View();
            }

            // POST: ClassifiedFiles/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Title,Description,InmateName,Felony,Sentence,IncarcerationDate,ExpectedReleaseDate")] ClassifiedFile classifiedFile)
            {
                if (ModelState.IsValid)
                {

                    classifiedFile.CreatedById = _classifiedFilesRepository.GetCurrentUserId();
                    classifiedFile.UpdatedById = _classifiedFilesRepository.GetCurrentUserId();
                    classifiedFile.CreatedAt = DateTime.Now;
                    classifiedFile.UpdatedAt = DateTime.Now;
                    _classifiedFilesRepository.Add(classifiedFile);
                    await _classifiedFilesRepository.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CreatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName", classifiedFile.CreatedById);
                ViewData["UpdatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName", classifiedFile.UpdatedById);
                return View(classifiedFile);
            }

            // GET: ClassifiedFiles/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var classifiedFile = await _classifiedFilesRepository.GetByIdAsync(id.GetValueOrDefault());
                if (classifiedFile == null)
                {
                    return NotFound();
                }
                ViewData["CreatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName", classifiedFile.CreatedById);
                ViewData["UpdatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName", classifiedFile.UpdatedById);
                return View(classifiedFile);
            }

            // POST: ClassifiedFiles/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,InmateName,Felony,Sentence,IncarcerationDate,ExpectedReleaseDate")] ClassifiedFile classifiedFile)
            {
                if (id != classifiedFile.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var originalFile = await _classifiedFilesRepository.GetByIdAsync(id);
                        classifiedFile.CreatedById = originalFile.CreatedById;
                        classifiedFile.CreatedAt = originalFile.CreatedAt;
                        classifiedFile.UpdatedById = _classifiedFilesRepository.GetCurrentUserId();
                        classifiedFile.UpdatedAt = DateTime.Now;
                        _classifiedFilesRepository.Update(classifiedFile);
                        await _classifiedFilesRepository.SaveAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClassifiedFileExists(classifiedFile.Id))
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
                ViewData["CreatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName", classifiedFile.CreatedById);
                ViewData["UpdatedById"] = new SelectList(_classifiedFilesRepository.GetUsers(), "Id", "UserName", classifiedFile.UpdatedById);
                return View(classifiedFile);
            }

            // GET: ClassifiedFiles/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var classifiedFile = await _classifiedFilesRepository.GetByIdAsync(id.GetValueOrDefault());
                if (classifiedFile == null)
                {
                    return NotFound();
                }

                return View(classifiedFile);
            }

            // POST: ClassifiedFiles/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _classifiedFilesRepository.Delete(id);
                await _classifiedFilesRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ClassifiedFileExists(int id)
            {
                return _classifiedFilesRepository.GetAll().Any(file => file.Id == id);
            }
        }
    }


