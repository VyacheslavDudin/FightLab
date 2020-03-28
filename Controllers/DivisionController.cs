using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using WebApplication2;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;
using BindAttribute = Microsoft.AspNetCore.Mvc.BindAttribute;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;
using ClosedXML;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;
using System.IO.Packaging;

namespace WebApplication2.Controllers
{
    public class DivisionController : Controller
    {
        private readonly FightContext _context;

        public DivisionController(FightContext context)
        {
            _context = context;
        }

        // GET: Division
        public async Task<IActionResult> Index()
        {
            return View(await _context.Division.ToListAsync());
        }

        // GET: Division/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Division
                .FirstOrDefaultAsync(m => m.Id == id);
            if (division == null)
            {
                return NotFound();
            }

            //return View(division);
            return RedirectToAction("Index", "Fighter", new {command="division", id = division.Id, name = division.Name });
        }

        // GET: Division/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Division/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Division division)
        {
            if (ModelState.IsValid)
            {
                if (_context.Division.Where(d => d.Name == division.Name).Count() != 0)
                {
                    ModelState.AddModelError("Name", "Вагова категорія з такою назвою вже існує");
                    return View(division);
                }
                
                _context.Add(division);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(division);
        }

        // GET: Division/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Division.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }
            ViewBag.Division = division.Name;
            return View(division);
        }

        // POST: Division/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Division division)
        {
            if (id != division.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Division.Where(d => d.Name == division.Name && d.Id!=id).Count() != 0)
                    {
                        ModelState.AddModelError("Name", "Вагова категорія з такою назвою вже існує");
                        return View(division);
                    }

                    _context.Update(division);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DivisionExists(division.Id))
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
            return View(division);
        }

        // GET: Division/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Division
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.FigtersCount = 0;
            ViewBag.FightersCount = _context.Fighter.Where(f => f.DivisionId == id).ToArray().Count();
            ViewBag.Division = division.Name;
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var division = await _context.Division.Include(d=>d.Fighter).FirstOrDefaultAsync(d=>d.Id==id);
            if (_context.Division.Count() == 1)
            {
                ModelState.AddModelError("Name", "Неможливо видалити всі вагові категорії. Додайте принаймні ще одну і спробуйте ще раз.");
                return View(division);
            }
            _context.Division.Remove(division);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DivisionExists(int id)
        {
            return _context.Division.Any(e => e.Id == id);
        }

     

    }
}
