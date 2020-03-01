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
    public class FightController : Controller
    {
        private readonly FightContext _context;

        public FightController(FightContext context)
        {
            _context = context;
        }

        // GET: Fight
        public async Task<IActionResult> Index()
        {
            var fightContext = _context.Fight.Include(f => f.Fighter1).Include(f => f.Fighter2);
            return View(await fightContext.ToListAsync());
        }

        // GET: Fight/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fight = await _context.Fight
                .Include(f => f.Fighter1)
                .Include(f => f.Fighter2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fight == null)
            {
                return NotFound();
            }

            return View(fight);
        }

        // GET: Fight/Create
        public IActionResult Create()
        {
            ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name");
            ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name");
            return View();
        }

        // POST: Fight/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Fighter1Id,Fighter2Id,Winner,TypeOfWin")] Fight fight)
        {
            if (ModelState.IsValid)
            {
                if (fight.Fighter1Id == fight.Fighter2Id)
                {
                    ModelState.AddModelError("Fighter2Id", "Неможливо додати бій бійця проти себе!");
                    ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
                    ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
                    return View(fight);
                }

                var fighter1 = _context.Fighter.Where(f => f.Id == fight.Fighter1Id).FirstOrDefaultAsync().Result;
                var fighter2 = _context.Fighter.Where(f => f.Id == fight.Fighter2Id).FirstOrDefaultAsync().Result;

                if (fight.Date != null)
                {
                    DateTime valDate1;
                    DateTime valDate2;

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

                    if (fighter2.Debut != null)
                    {
                        valDate2 = (DateTime)(fighter2.Debut);
                    }
                    else if (fighter2.DateOfBirth != null)
                    {
                        valDate2 = ((DateTime)(fighter2.DateOfBirth)).AddYears(18);
                    }
                    else
                    {
                        valDate2 = DateTime.Now;
                    }

                    if (DateTime.Compare(valDate1, (DateTime)(fight.Date)) > 0 || DateTime.Compare(valDate2, (DateTime)(fight.Date)) > 0 || DateTime.Compare((DateTime)(fight.Date), DateTime.Now) > 0)
                    {
                        ModelState.AddModelError("Date", "Некоректна дата бою!");
                        ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
                        ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
                        return View(fight);
                    }
                }

                if (_context.Fight.Where(f => ((f.Fighter1Id == fighter1.Id) || (f.Fighter2Id == fighter1.Id))&& ((f.Fighter1Id == fighter2.Id) || (f.Fighter2Id == fighter2.Id))&&(f.Date==fight.Date)  ).ToArray().Count() != 0)
                {
                    ModelState.AddModelError("Date", "Такий бій вже існує!");
                    ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
                    ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
                    return View(fight);
                }
                _context.Add(fight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
            ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
            return View(fight);
        }

        // GET: Fight/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fight = await _context.Fight.FindAsync(id);
            if (fight == null)
            {
                return NotFound();
            }
            ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
            ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
            return View(fight);
        }

        // POST: Fight/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Fighter1Id,Fighter2Id,Winner,TypeOfWin")] Fight fight)
        {
            if (id != fight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fight.Fighter1Id == fight.Fighter2Id)
                    {
                        ModelState.AddModelError("Fighter2Id", "Неможливо додати бій бійця проти себе!");
                        ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
                        ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
                        return View(fight);
                    }

                    var fighter1 = _context.Fighter.Where(f => f.Id == fight.Fighter1Id).FirstOrDefaultAsync().Result;
                    var fighter2 = _context.Fighter.Where(f => f.Id == fight.Fighter2Id).FirstOrDefaultAsync().Result;

                    if (fight.Date != null)
                    {
                        DateTime valDate1;
                        DateTime valDate2;

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

                        if (fighter2.Debut != null)
                        {
                            valDate2 = (DateTime)(fighter2.Debut);
                        }
                        else if (fighter2.DateOfBirth != null)
                        {
                            valDate2 = ((DateTime)(fighter2.DateOfBirth)).AddYears(18);
                        }
                        else
                        {
                            valDate2 = DateTime.Now;
                        }

                        if (DateTime.Compare(valDate1, (DateTime)(fight.Date)) > 0 || DateTime.Compare(valDate2, (DateTime)(fight.Date)) > 0 || DateTime.Compare((DateTime)(fight.Date), DateTime.Now ) > 0)
                        {
                            ModelState.AddModelError("Date", "Некоректна дата бою!");
                            ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
                            ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
                            return View(fight);
                        }
                    }

                    if (_context.Fight.Where(f => ((f.Fighter1Id == fighter1.Id) || (f.Fighter2Id == fighter1.Id)) && ((f.Fighter1Id == fighter2.Id) || (f.Fighter2Id == fighter2.Id)) && (f.Date == fight.Date)&&(f.Id!=fight.Id) ).ToArray().Count() != 0)
                    {
                        ModelState.AddModelError("Date", "Такий бій вже існує!");
                        ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
                        ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
                        return View(fight);
                    }

                    _context.Update(fight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FightExists(fight.Id))
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
            ViewData["Fighter1Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter1Id);
            ViewData["Fighter2Id"] = new SelectList(_context.Fighter, "Id", "Name", fight.Fighter2Id);
            return View(fight);
        }

        // GET: Fight/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fight = await _context.Fight
                .Include(f => f.Fighter1)
                .Include(f => f.Fighter2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fight == null)
            {
                return NotFound();
            }

            return View(fight);
        }

        // POST: Fight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fight = await _context.Fight.FindAsync(id);
            _context.Fight.Remove(fight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FightExists(int id)
        {
            return _context.Fight.Any(e => e.Id == id);
        }
    }
}
