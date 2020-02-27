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
        public async Task<IActionResult> Create([Bind("FighterId,TitleId,DateOfGettingTitle")] TitleHolders titleHolders)
        {
            if (ModelState.IsValid)
            {
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
            if (titleHolders == null)
            {
                return NotFound();
            }
            ViewData["FighterId"] = new SelectList(_context.Fighter, "Id", "Name", titleHolders.FighterId);
            ViewData["TitleId"] = new SelectList(_context.Title, "Id", "Name", titleHolders.TitleId);
            return View(titleHolders);
        }

        // POST: TitleHolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FighterId,TitleId,DateOfGettingTitle")] TitleHolders titleHolders)
        {
            if (id != titleHolders.TitleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
