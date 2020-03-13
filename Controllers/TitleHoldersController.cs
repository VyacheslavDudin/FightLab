using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class TitleHoldersController : Controller
    {
        private readonly FightContext _context;

        public TitleHoldersController(FightContext context)
        {
            _context = context;
        }

        // GET: TitleHolders
        public async Task<IActionResult> Index()
        {
          //  var fightContext = _context.Title.Include(t => t.Id).Include(t => t.TitleHolders.FighterId).ToListAsync();
           var fightContext = _context.TitleHolders.Include(t => t.Fighter).Include(t => t.Title);
         
            return View(await fightContext.ToListAsync());
        }

        // GET: TitleHolders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleHolders = await _context.TitleHolders
                .Include(t => t.Fighter)
                .Include(t => t.Title)
                .FirstOrDefaultAsync(m => m.TitleId == id);
            @ViewBag.TitleName = _context.Title.Where(t => t.Id == id).FirstOrDefaultAsync().Result.Name;
            if (titleHolders == null)
            {
                return NotFound();
            }
           
            return View(titleHolders);
        }

        // GET: TitleHolders/Create
        public IActionResult Create()
        {
  
            ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name");
            ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name");
            return View();
        }

        // POST: TitleHolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FighterId,TitleId,DateOfGettingTitle")] TitleHolders titleHolders, string TitleName)
        {
            Title title = new Title();
            title.Id = 0;
            title.Name = TitleName;
            if (ModelState.IsValid)
            {
                if (titleHolders.FighterId == 0)
                {
                    titleHolders.FighterId = null;
                }
               
                if(_context.Title.Where(t=>t.Name==title.Name).ToListAsync().Result.Count() != 0)
                {//ERROR привязать помилку до спана, видалити титули
                    ModelState.AddModelError("TitleName", "Титул з таким ім\'ям вже існує");
                    ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
                    ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
                    return View(titleHolders);
                }

                if (titleHolders.DateOfGettingTitle != null)
                {
                    var fighter1 = _context.Fighter.Where(f => f.Id == titleHolders.FighterId).FirstOrDefaultAsync().Result;
                    DateTime valDate1;

                    if (fighter1.Debut != null)
                    {
                        valDate1 = (DateTime)(fighter1.Debut);
                    }
                    else if (fighter1.DateOfBirth != null)
                    {
                        valDate1 = ((DateTime)(fighter1.DateOfBirth)).AddYears(18);
                    }
                    else
                    {
                        valDate1 = DateTime.Now;
                    }
                    if (DateTime.Compare(valDate1, (DateTime)(titleHolders.DateOfGettingTitle)) > 0 || DateTime.Compare(DateTime.Now, (DateTime)(titleHolders.DateOfGettingTitle)) > 0)
                    {
                        ModelState.AddModelError("DateOfGettingTitle", "Перевірте коректність даних! Для цього бійця ця дата є некоректною");
                        ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
                        ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
                        return View(titleHolders);
                    }
                }

                _context.Add(title);
                await _context.SaveChangesAsync();
                titleHolders.TitleId = _context.Title.Where(t => t.Name == TitleName).FirstOrDefaultAsync().Result.Id;
                _context.Add(titleHolders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
            ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
            return View(titleHolders);
        }

        // GET: TitleHolders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleHolders = await _context.TitleHolders.FindAsync(id);
            @ViewBag.TitleName = _context.Title.Where(t => t.Id == id).FirstOrDefaultAsync().Result.Name;
            if (titleHolders == null)
            {
                return NotFound();
            }
            @ViewBag.TitleName = _context.Title.Where(t=>t.Id== titleHolders.TitleId).FirstOrDefaultAsync().Result.Name;
            ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
            ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
            return View(titleHolders);
        }

        // POST: TitleHolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FighterId,TitleId,DateOfGettingTitle,TitleName ")] TitleHolders titleHolders, string TitleName)
        {
            if (id != titleHolders.TitleId)
            {
                return NotFound();
            }
            Title title = new Title();
            title.Id = titleHolders.TitleId;
            title.Name = TitleName;
            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (titleHolders.FighterId == 0)
                    {
                        titleHolders.FighterId = null;
                    }

                    if (titleHolders.FighterId == 0)
                    {
                        titleHolders.FighterId = null;
                    }

                    if (_context.Title.Where(t => t.Name == title.Name && t.Id != title.Id).ToListAsync().Result.Count() != 0)
                    {
                        ModelState.AddModelError("TitleName", "Титул з таким ім\'ям вже існує");
                        ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
                        ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
                        return View(titleHolders);
                    }

                    if (titleHolders.DateOfGettingTitle != null)
                    {
                        var fighter1 = _context.Fighter.Where(f => f.Id == titleHolders.FighterId).FirstOrDefaultAsync().Result;
                        DateTime valDate1;

                        if (fighter1.Debut != null)
                        {
                            valDate1 = (DateTime)(fighter1.Debut);
                        }
                        else if (fighter1.DateOfBirth != null)
                        {
                            valDate1 = ((DateTime)(fighter1.DateOfBirth)).AddYears(18);
                        }
                        else
                        {
                            valDate1 = DateTime.Now;
                        }

                        if (DateTime.Compare(valDate1, (DateTime)(titleHolders.DateOfGettingTitle)) > 0 || DateTime.Compare(DateTime.Now, (DateTime)(titleHolders.DateOfGettingTitle)) > 0)
                        {
                            ModelState.AddModelError("DateOfGettingTitle", "Перевірте коректність даних! Для цього бійця ця дата є некоректною");
                            ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
                            ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
                            return View(titleHolders);
                        }
                    }
    
                        _context.Update(title);
                    _context.Update(titleHolders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitleHoldersExists(titleHolders.TitleId))
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
            ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
            ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
            return View(titleHolders);
        }

        // GET: TitleHolders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleHolders = await _context.TitleHolders
                .Include(t => t.Fighter)
                .Include(t => t.Title)
                .FirstOrDefaultAsync(m => m.TitleId == id);
            @ViewBag.TitleName = _context.Title.Where(t => t.Id == id).FirstOrDefaultAsync().Result.Name;
            if (titleHolders == null)
            {
                return NotFound();
            }

            return View(titleHolders);
        }

        // POST: TitleHolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var titleHolders = await _context.TitleHolders.FindAsync(id);
            _context.TitleHolders.Remove(titleHolders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TitleHoldersExists(int id)
        {
            return _context.TitleHolders.Any(e => e.TitleId == id);
        }
    }
}
