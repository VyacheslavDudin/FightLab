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
    public class FighterController : Controller
    {
        private readonly FightContext _context;

        public FighterController(FightContext context)
        {
            _context = context;
        }

        // GET: Fighter
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Division", "Index");
            //Знаходження бійців за ваг.категор.
            ViewBag.DivisionId = id;
            ViewBag.DivisionName = name;

            var fightersByDivision = _context.Fighter.Where(f => f.DivisionId == id).Include(f => f.Division).Include(f => f.Status).Include(f => f.Country);


            //var fightContext = _context.Fighter.Include(f => f.Country).Include(f => f.Division).Include(f => f.Status);
            return View(await fightersByDivision.ToListAsync());
        }

        // GET: Fighter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fighter = await _context.Fighter
                .Include(f => f.Country)
                .Include(f => f.Division)
                .Include(f => f.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fighter == null)
            {
                return NotFound();
            }

            return View(fighter);
        }

        // GET: Fighter/Create
        public IActionResult Create(int divisionId, string divisionName)
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            //ViewData["DivisionId"] = new SelectList(_context.Division, "Id", "Name");
            ////ViewBag.DivisionId = divisionId;
            ////ViewBag.DivisionName = divisionName;
            ViewData["DivisionId"] = new SelectList(_context.Division, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name");
            return View();
        }

        // POST: Fighter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int divisionId, [Bind("Id,DivisionId,DateOfBirth,CountryId,StatusId,Height,Weight,Debut,Name")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fighter);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Fighter", new { id = divisionId, name = _context.Division.Where(d => d.Id == divisionId).FirstOrDefault().Name });
            }
            /*ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", fighter.CountryId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "Id", "Name", fighter.DivisionId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", fighter.StatusId);
            return View(fighter);*/
            return RedirectToAction("Index", "Fighter", new { id = divisionId, countryId = _context.Country.Where(c => c.Id == fighter.CountryId).FirstOrDefault().Name, 
                statusId = _context.Status.Where(s => s.Id == fighter.StatusId).FirstOrDefault().Name });
        }

        // GET: Fighter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fighter = await _context.Fighter.FindAsync(id);
            if (fighter == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", fighter.CountryId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "Id", "Name", fighter.DivisionId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", fighter.StatusId);
            return View(fighter);
        }

        // POST: Fighter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivisionId,DateOfBirth,CountryId,StatusId,Height,Weight,Debut,Name")] Fighter fighter)
        {
            if (id != fighter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fighter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FighterExists(fighter.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", fighter.CountryId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "Id", "Name", fighter.DivisionId);
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name", fighter.StatusId);
            return View(fighter);
        }

        // GET: Fighter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fighter = await _context.Fighter
                .Include(f => f.Country)
                .Include(f => f.Division)
                .Include(f => f.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fighter == null)
            {
                return NotFound();
            }

            return View(fighter);
        }

        // POST: Fighter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fighter = await _context.Fighter.FindAsync(id);
            _context.Fighter.Remove(fighter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FighterExists(int id)
        {
            return _context.Fighter.Any(e => e.Id == id);
        }
    }
}
