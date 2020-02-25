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
        public async Task<IActionResult> Index(string? command, int? id, string? name)
        {
            switch (command)
            {
                case "division":
                    {
                        if (id == null) return RedirectToAction("Division", "Index");
                        //Знаходження бійців за ваг.категор.
                        ViewBag.Id = id;
                        ViewBag.Name = name;
                        ViewBag.Parametr = "ваг.категор.";
                        ViewBag.Command = "division";
                        

                        var fightersByDivision = _context.Fighter.Where(f => f.DivisionId == id).Include(f => f.Division).Include(f => f.Status).Include(f => f.Country);


                        //var fightContext = _context.Fighter.Include(f => f.Country).Include(f => f.Division).Include(f => f.Status);
                        return View(await fightersByDivision.ToListAsync());
                    }
                case "country":
                    {
                        if (id == null) return RedirectToAction("Countries", "Index");
                        //Знаходження бійців за country
                        ViewBag.Id = id;
                        ViewBag.Name = name;
                        ViewBag.Parametr = "країною";
                        ViewBag.Command = "country";

                        var fightersByCountry = _context.Fighter.Where(f => f.CountryId == id).Include(f => f.Division).Include(f => f.Status).Include(f => f.Country);


                        //var fightContext = _context.Fighter.Include(f => f.Country).Include(f => f.Division).Include(f => f.Status);
                        return View(await fightersByCountry.ToListAsync());
                    }
                case "status":
                    {
                        if (id == null) return RedirectToAction("Status", "Index");
                        //Знаходження бійців за status
                        ViewBag.Id = id;
                        ViewBag.Name = name;
                        ViewBag.Parametr = "статусом";
                        ViewBag.Command = "status";

                        var fightersByStatus = _context.Fighter.Where(f => f.StatusId == id).Include(f => f.Division).Include(f => f.Status).Include(f => f.Country);


                        //var fightContext = _context.Fighter.Include(f => f.Country).Include(f => f.Division).Include(f => f.Status);
                        return View(await fightersByStatus.ToListAsync());
                    }
                case "fight":
                    {
                        var fightContext = _context.Fighter.Include(f => f.Country).Include(f => f.Division).Include(f => f.Status);
                        return View(await fightContext.ToListAsync());
                    }
                case "title":
                    {
                        var fightContext = _context.Fighter.Include(f => f.Country).Include(f => f.Division).Include(f => f.Status);
                        return View(await fightContext.ToListAsync());
                    }
                default:
                    {
                        var fightContext = _context.Fighter.Include(f => f.Country).Include(f => f.Division).Include(f => f.Status);
                        return View(await fightContext.ToListAsync());
                    }
            }
            
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
        public IActionResult Create(string command, int id, string name, string url)
        {
            switch (command)
            {
                case "division":
                    {
                        ViewBag.Parametr = "ваг.кат";
                        ViewBag.InputId = "DivisionId";
                        ViewBag.Id = id;
                        ViewBag.Name = _context.Division.Where(d => d.Id == id).FirstOrDefaultAsync().Result.Name;
                        break;
                    }
                case "country":
                    {
                        ViewBag.Parametr = "країни";
                        ViewBag.InputId = "CountryId";
                        ViewBag.Id = id;
                        ViewBag.Name = _context.Country.Where(c => c.Id == id).FirstOrDefaultAsync().Result.Name;
                        break;
                    }
                case "status":
                    {
                        ViewBag.Parametr = "статусу";
                        ViewBag.InputId = "StatusId";
                        ViewBag.Id = id;
                        ViewBag.Name = _context.Status.Where(s => s.Id == id).FirstOrDefaultAsync().Result.Name;
                        break;
                    }
           
                default:
                    {
                        ViewBag.Parametr = "";
                        break;
                    }
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            //ViewData["DivisionId"] = new SelectList(_context.Division, "Id", "Name");
            ////ViewBag.DivisionId = divisionId;
            ////ViewBag.DivisionName = divisionName;
            ViewData["DivisionId"] = new SelectList(_context.Division, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Name");
            ViewBag.url = url;
            return View();
        }

        // POST: Fighter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string command, int divisionId, [Bind("Id,DivisionId,DateOfBirth,CountryId,StatusId,Height,Weight,Debut,Name")] Fighter fighter)
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
