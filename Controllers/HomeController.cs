using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;
using ClosedXML;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;
using System.IO.Packaging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using WebApplication2;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;
using BindAttribute = Microsoft.AspNetCore.Mvc.BindAttribute;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FightContext _context;

        public HomeController(ILogger<HomeController> logger, FightContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Diagram()
        {
            return View();
        }
        public IActionResult Report()
        {
            return View();
        }

        // POST: Division/Import/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileForm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (fileForm != null)
                    {
                        using (var stream = new FileStream(fileForm.FileName, FileMode.Create))
                        {
                            await fileForm.CopyToAsync(stream);
                            using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                            {
                                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                                {

                                    foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                    {
                                        try
                                        {
                                            Fighter fighter = new Fighter();


                                            if (row.Cell(1).Value.ToString().Trim().Length == 0)
                                                continue;
                                            fighter.Name = row.Cell(1).Value.ToString().Trim();

                                            if (row.Cell(2).Value.ToString().Trim().Length == 0)
                                                continue;
                                            if (row.Cell(2).Value.ToString().Trim().Length > 0)
                                            {
                                                Division division;

                                                var di = (from div in _context.Division
                                                          where div.Name.Contains(row.Cell(2).Value.ToString())
                                                          select div).ToList();
                                                if (di.Count > 0)
                                                {
                                                    division = di[0];
                                                    fighter.DivisionId = _context.Division.Where(d => d.Name == division.Name).FirstOrDefault().Id;
                                                }
                                                else
                                                {
                                                    division = new Division();
                                                    division.Name = row.Cell(2).Value.ToString().Trim();
                                                    division.Id = 0;
                                                    _context.Add(division);
                                                    await _context.SaveChangesAsync();
                                                    fighter.DivisionId = _context.Division.Where(d => d.Name == division.Name).FirstOrDefault().Id;
                                                }
                                            }

                                            if (row.Cell(3).Value.ToString().Trim().Length == 0)
                                                continue;
                                            if (row.Cell(3).Value.ToString().Trim().Length > 0)
                                            {
                                                Country country;

                                                var c = (from coun in _context.Country
                                                         where coun.Name.Contains(row.Cell(3).Value.ToString())
                                                         select coun).ToList();
                                                if (c.Count > 0)
                                                {
                                                    country = c[0];
                                                    fighter.CountryId = _context.Country.Where(c => c.Name == country.Name).FirstOrDefault().Id;
                                                }
                                                else
                                                {
                                                    country = new Country();
                                                    country.Name = row.Cell(3).Value.ToString();
                                                    country.Id = 0;
                                                    _context.Add(country);
                                                    await _context.SaveChangesAsync();
                                                    fighter.CountryId = _context.Country.Where(c => c.Name == country.Name).FirstOrDefault().Id;
                                                }
                                            }

                                            if (row.Cell(4).Value.ToString().Trim().Length == 0)
                                                continue;
                                            if (row.Cell(4).Value.ToString().Trim().Length > 0)
                                            {
                                                Status status;

                                                var s = (from st in _context.Status
                                                         where st.Name.Contains(row.Cell(4).Value.ToString())
                                                         select st).ToList();
                                                if (s.Count > 0)
                                                {
                                                    status = s[0];
                                                    fighter.StatusId = _context.Status.Where(s => s.Name == status.Name).FirstOrDefault().Id;
                                                }
                                                else
                                                {
                                                    status = new Status();
                                                    status.Name = row.Cell(4).Value.ToString();
                                                    status.Id = 0;
                                                    _context.Add(status);
                                                    await _context.SaveChangesAsync();
                                                    fighter.StatusId = _context.Status.Where(s => s.Name == status.Name).FirstOrDefault().Id;
                                                }

                                            }



                                            int height;
                                            if (int.TryParse(row.Cell(5).Value.ToString(), out height))
                                            {
                                                if (height >= 120 && height <= 250)
                                                {
                                                    fighter.Height = height;
                                                }
                                            }


                                            int weight;
                                            if (int.TryParse(row.Cell(6).Value.ToString(), out weight))
                                            {
                                                if (weight >= 50 && weight <= 350)
                                                {
                                                    fighter.Weight = weight;
                                                }
                                            }


                                            DateTime date;
                                            if (DateTime.TryParse(row.Cell(7).Value.ToString(), out date))
                                            {
                                                if (DateTime.Compare(date.AddYears(18), DateTime.Now) < 0)
                                                {
                                                    fighter.DateOfBirth = date;
                                                }
                                            }

                                            DateTime dateDebut;
                                            if (DateTime.TryParse(row.Cell(8).Value.ToString(), out dateDebut))
                                            {
                                                if (DateTime.Compare(dateDebut, DateTime.Now) < 0)
                                                {
                                                    if (date != null)
                                                    {
                                                        if (DateTime.Compare(date.AddYears(18), dateDebut) < 0)
                                                        {
                                                            fighter.Debut = dateDebut;
                                                        }
                                                    }
                                                    else fighter.Debut = dateDebut;


                                                }
                                            }

                                            _context.Fighter.Add(fighter);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("ERROR");
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("ERROR");
                }
                await _context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public Microsoft.AspNetCore.Mvc.ActionResult Export(string loadReport)
        {
            

            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
              
                switch (loadReport)
                {
                    case "All":
                        {
                            var groupedBy = _context.Fighter.ToList();
                                var worksheet = workbook.Worksheets.Add("Всі бійці");
                                worksheet.Cell("A1").Value = "П.І.Б.";
                                worksheet.Cell("B1").Value = "Division";
                                worksheet.Cell("C1").Value = "Country";
                                worksheet.Cell("D1").Value = "Status";
                                worksheet.Cell("E1").Value = "Height";
                                worksheet.Cell("F1").Value = "Weight";
                                worksheet.Cell("G1").Value = "DateOfBirth";
                                worksheet.Cell("H1").Value = "Debut";
                                worksheet.Row(1).Style.Font.Bold = true;
                                var fighters = groupedBy;
                                //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                                for (int i = 0; i < fighters.Count; i++)
                                {
                                    worksheet.Cell(i + 2, 1).Value = fighters[i].Name;
                                    worksheet.Cell(i + 2, 2).Value = _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 3).Value = _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 4).Value = _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 5).Value = fighters[i].Height;
                                    worksheet.Cell(i + 2, 6).Value = fighters[i].Weight;
                                    worksheet.Cell(i + 2, 7).Value = fighters[i].DateOfBirth.ToString();
                                    worksheet.Cell(i + 2, 8).Value = fighters[i].Debut.ToString();

                                }
                            
                            break;
                        }
                    case "Division":
                        {
                            var groupedBy = _context.Division.Include("Fighter").ToList();
                            foreach (var d in groupedBy)
                            {
                                var worksheet = workbook.Worksheets.Add(d.Name);
                                worksheet.Cell("A1").Value = "П.І.Б.";
                                worksheet.Cell("B1").Value = "Division";
                                worksheet.Cell("C1").Value = "Country";
                                worksheet.Cell("D1").Value = "Status";
                                worksheet.Cell("E1").Value = "Height";
                                worksheet.Cell("F1").Value = "Weight";
                                worksheet.Cell("G1").Value = "DateOfBirth";
                                worksheet.Cell("H1").Value = "Debut";
                                worksheet.Row(1).Style.Font.Bold = true;
                                var fighters = d.Fighter.ToList();
                                //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                                for (int i = 0; i < fighters.Count; i++)
                                {
                                    worksheet.Cell(i + 2, 1).Value = fighters[i].Name;
                                    worksheet.Cell(i + 2, 2).Value = _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 3).Value = _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 4).Value = _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 5).Value = fighters[i].Height;
                                    worksheet.Cell(i + 2, 6).Value = fighters[i].Weight;
                                    worksheet.Cell(i + 2, 7).Value = fighters[i].DateOfBirth.ToString();
                                    worksheet.Cell(i + 2, 8).Value = fighters[i].Debut.ToString();

                                }
                            }
                            break;
                        }
                    case "Country":
                        {
                            var groupedBy = _context.Country.Include("Fighter").ToList();
                            foreach (var d in groupedBy)
                            {
                                var worksheet = workbook.Worksheets.Add(d.Name);
                                worksheet.Cell("A1").Value = "П.І.Б.";
                                worksheet.Cell("B1").Value = "Division";
                                worksheet.Cell("C1").Value = "Country";
                                worksheet.Cell("D1").Value = "Status";
                                worksheet.Cell("E1").Value = "Height";
                                worksheet.Cell("F1").Value = "Weight";
                                worksheet.Cell("G1").Value = "DateOfBirth";
                                worksheet.Cell("H1").Value = "Debut";
                                worksheet.Row(1).Style.Font.Bold = true;
                                var fighters = d.Fighter.ToList();
                                //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                                for (int i = 0; i < fighters.Count; i++)
                                {
                                    worksheet.Cell(i + 2, 1).Value = fighters[i].Name;
                                    worksheet.Cell(i + 2, 2).Value = _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 3).Value = _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 4).Value = _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 5).Value = fighters[i].Height;
                                    worksheet.Cell(i + 2, 6).Value = fighters[i].Weight;
                                    worksheet.Cell(i + 2, 7).Value = fighters[i].DateOfBirth.ToString();
                                    worksheet.Cell(i + 2, 8).Value = fighters[i].Debut.ToString();

                                }
                            }
                            break;
                        }
                    case "Status":
                        {
                            var groupedBy = _context.Status.Include("Fighter").ToList();
                            foreach (var d in groupedBy)
                            {
                                var worksheet = workbook.Worksheets.Add(d.Name);
                                worksheet.Cell("A1").Value = "П.І.Б.";
                                worksheet.Cell("B1").Value = "Division";
                                worksheet.Cell("C1").Value = "Country";
                                worksheet.Cell("D1").Value = "Status";
                                worksheet.Cell("E1").Value = "Height";
                                worksheet.Cell("F1").Value = "Weight";
                                worksheet.Cell("G1").Value = "DateOfBirth";
                                worksheet.Cell("H1").Value = "Debut";
                                worksheet.Row(1).Style.Font.Bold = true;
                                var fighters = d.Fighter.ToList();
                                //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                                for (int i = 0; i < fighters.Count; i++)
                                {
                                    worksheet.Cell(i + 2, 1).Value = fighters[i].Name;
                                    worksheet.Cell(i + 2, 2).Value = _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 3).Value = _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 4).Value = _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 5).Value = fighters[i].Height;
                                    worksheet.Cell(i + 2, 6).Value = fighters[i].Weight;
                                    worksheet.Cell(i + 2, 7).Value = fighters[i].DateOfBirth.ToString();
                                    worksheet.Cell(i + 2, 8).Value = fighters[i].Debut.ToString();

                                }
                            }
                            break;
                        }
                    default:
                        {
                            var groupedBy = _context.Fighter.ToList();
                                var worksheet = workbook.Worksheets.Add("Всі бійці");
                                worksheet.Cell("A1").Value = "П.І.Б.";
                                worksheet.Cell("B1").Value = "Division";
                                worksheet.Cell("C1").Value = "Country";
                                worksheet.Cell("D1").Value = "Status";
                                worksheet.Cell("E1").Value = "Height";
                                worksheet.Cell("F1").Value = "Weight";
                                worksheet.Cell("G1").Value = "DateOfBirth";
                                worksheet.Cell("H1").Value = "Debut";
                                worksheet.Row(1).Style.Font.Bold = true;
                                var fighters = groupedBy;
                                //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                                for (int i = 0; i < fighters.Count; i++)
                                {
                                    worksheet.Cell(i + 2, 1).Value = fighters[i].Name;
                                    worksheet.Cell(i + 2, 2).Value = _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 3).Value = _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 4).Value = _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name;
                                    worksheet.Cell(i + 2, 5).Value = fighters[i].Height;
                                    worksheet.Cell(i + 2, 6).Value = fighters[i].Weight;
                                    worksheet.Cell(i + 2, 7).Value = fighters[i].DateOfBirth.ToString();
                                    worksheet.Cell(i + 2, 8).Value = fighters[i].Debut.ToString();

                                }
                            
                            break;
                        }
                }


                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new Microsoft.AspNetCore.Mvc.FileContentResult(stream.ToArray(),
                      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"fighters_{ DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
        //------------------------------------------------------------------------
        //------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------
        //------------------------------------------------------------------------
        // POST: Division/Import/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportDoc(IFormFile fileForm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (fileForm != null)
                    {
                        using (var stream = new FileStream(fileForm.FileName, FileMode.Create))
                        {
                            await fileForm.CopyToAsync(stream);
                            Document doc = new Document();
                            doc.LoadFromStream(stream, FileFormat.Docx);
                            var table = doc.Sections[0].Tables[0];
                            var rows = table.Rows;
                            for(int i=1; i<rows.Count; i++)
                            {
                                Fighter fighter = new Fighter();


                                if (rows[i].Cells[0].Paragraphs[0].Text.ToString().Trim().Length == 0)
                                    continue;
                                fighter.Name = rows[i].Cells[0].Paragraphs[0].Text.ToString().Trim();

                                if (rows[i].Cells[1].Paragraphs[0].Text.ToString().Trim().Length == 0)
                                    continue;
                                if (rows[i].Cells[1].Paragraphs[0].Text.ToString().Trim().Length > 0)
                                {
                                    Division division;

                                    var di = (from div in _context.Division
                                              where div.Name.Contains(rows[i].Cells[1].Paragraphs[0].Text.ToString())
                                              select div).ToList();
                                    if (di.Count > 0)
                                    {
                                        division = di[0];
                                        fighter.DivisionId = _context.Division.Where(d => d.Name == division.Name).FirstOrDefault().Id;
                                    }
                                    else
                                    {
                                        division = new Division();
                                        division.Name = rows[i].Cells[1].Paragraphs[0].Text.ToString().Trim();
                                        division.Id = 0;
                                        _context.Add(division);
                                        await _context.SaveChangesAsync();
                                        fighter.DivisionId = _context.Division.Where(d => d.Name == division.Name).FirstOrDefault().Id;
                                    }
                                }

                                if (rows[i].Cells[2].Paragraphs[0].Text.ToString().Trim().Length == 0)
                                    continue;
                                if (rows[i].Cells[2].Paragraphs[0].Text.ToString().Trim().Length > 0)
                                {
                                    Country country;

                                    var c = (from coun in _context.Country
                                             where coun.Name.Contains(rows[i].Cells[2].Paragraphs[0].Text.ToString())
                                             select coun).ToList();
                                    if (c.Count > 0)
                                    {
                                        country = c[0];
                                        fighter.CountryId = _context.Country.Where(c => c.Name == country.Name).FirstOrDefault().Id;
                                    }
                                    else
                                    {
                                        country = new Country();
                                        country.Name = rows[i].Cells[2].Paragraphs[0].Text.ToString();
                                        country.Id = 0;
                                        _context.Add(country);
                                        await _context.SaveChangesAsync();
                                        fighter.CountryId = _context.Country.Where(c => c.Name == country.Name).FirstOrDefault().Id;
                                    }
                                }

                                if (rows[i].Cells[3].Paragraphs[0].Text.ToString().Trim().Length == 0)
                                    continue;
                                if (rows[i].Cells[3].Paragraphs[0].Text.ToString().Trim().Length > 0)
                                {
                                    Status status;

                                    var s = (from st in _context.Status
                                             where st.Name.Contains(rows[i].Cells[3].Paragraphs[0].Text.ToString())
                                             select st).ToList();
                                    if (s.Count > 0)
                                    {
                                        status = s[0];
                                        fighter.StatusId = _context.Status.Where(s => s.Name == status.Name).FirstOrDefault().Id;
                                    }
                                    else
                                    {
                                        status = new Status();
                                        status.Name = rows[i].Cells[3].Paragraphs[0].Text.ToString();
                                        status.Id = 0;
                                        _context.Add(status);
                                        await _context.SaveChangesAsync();
                                        fighter.StatusId = _context.Status.Where(s => s.Name == status.Name).FirstOrDefault().Id;
                                    }

                                }



                                int height;
                                if (int.TryParse(rows[i].Cells[4].Paragraphs[0].Text.ToString(), out height))
                                {
                                    if (height >= 120 && height <= 250)
                                    {
                                        fighter.Height = height;
                                    }
                                }


                                int weight;
                                if (int.TryParse(rows[i].Cells[5].Paragraphs[0].Text.ToString(), out weight))
                                {
                                    if (weight >= 50 && weight <= 350)
                                    {
                                        fighter.Weight = weight;
                                    }
                                }


                                DateTime date;
                                if (DateTime.TryParse(rows[i].Cells[6].Paragraphs[0].Text.ToString(), out date))
                                {
                                    if (DateTime.Compare(date.AddYears(18), DateTime.Now) < 0)
                                    {
                                        fighter.DateOfBirth = date;
                                    }
                                }

                                DateTime dateDebut;
                                if (DateTime.TryParse(rows[i].Cells[7].Paragraphs[0].Text.ToString(), out dateDebut))
                                {
                                    if (DateTime.Compare(dateDebut, DateTime.Now) < 0)
                                    {
                                        if (date != null)
                                        {
                                            if (DateTime.Compare(date.AddYears(18), dateDebut) < 0)
                                            {
                                                fighter.Debut = dateDebut;
                                            }
                                        }
                                        else fighter.Debut = dateDebut;


                                    }
                                }

                                _context.Fighter.Add(fighter);
                            }

                            //using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                            //{
                            //    foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            //    {

                            //        foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                            //        {
                            //            try
                            //            {
                            //Fighter fighter = new Fighter();


                            //if (row.Cell(1).Value.ToString().Trim().Length == 0)
                            //    continue;
                            //fighter.Name = row.Cell(1).Value.ToString().Trim();

                            //if (row.Cell(2).Value.ToString().Trim().Length == 0)
                            //    continue;
                            //if (row.Cell(2).Value.ToString().Trim().Length > 0)
                            //{
                            //    Division division;

                            //    var di = (from div in _context.Division
                            //              where div.Name.Contains(row.Cell(2).Value.ToString())
                            //              select div).ToList();
                            //    if (di.Count > 0)
                            //    {
                            //        division = di[0];
                            //        fighter.DivisionId = _context.Division.Where(d => d.Name == division.Name).FirstOrDefault().Id;
                            //    }
                            //    else
                            //    {
                            //        division = new Division();
                            //        division.Name = row.Cell(2).Value.ToString().Trim();
                            //        division.Id = 0;
                            //        _context.Add(division);
                            //        await _context.SaveChangesAsync();
                            //        fighter.DivisionId = _context.Division.Where(d => d.Name == division.Name).FirstOrDefault().Id;
                            //    }
                            //}

                            //if (row.Cell(3).Value.ToString().Trim().Length == 0)
                            //    continue;
                            //if (row.Cell(3).Value.ToString().Trim().Length > 0)
                            //{
                            //    Country country;

                            //    var c = (from coun in _context.Country
                            //             where coun.Name.Contains(row.Cell(3).Value.ToString())
                            //             select coun).ToList();
                            //    if (c.Count > 0)
                            //    {
                            //        country = c[0];
                            //        fighter.CountryId = _context.Country.Where(c => c.Name == country.Name).FirstOrDefault().Id;
                            //    }
                            //    else
                            //    {
                            //        country = new Country();
                            //        country.Name = row.Cell(3).Value.ToString();
                            //        country.Id = 0;
                            //        _context.Add(country);
                            //        await _context.SaveChangesAsync();
                            //        fighter.CountryId = _context.Country.Where(c => c.Name == country.Name).FirstOrDefault().Id;
                            //    }
                            //}

                            //if (row.Cell(4).Value.ToString().Trim().Length == 0)
                            //    continue;
                            //if (row.Cell(4).Value.ToString().Trim().Length > 0)
                            //{
                            //    Status status;

                            //    var s = (from st in _context.Status
                            //             where st.Name.Contains(row.Cell(4).Value.ToString())
                            //             select st).ToList();
                            //    if (s.Count > 0)
                            //    {
                            //        status = s[0];
                            //        fighter.StatusId = _context.Status.Where(s => s.Name == status.Name).FirstOrDefault().Id;
                            //    }
                            //    else
                            //    {
                            //        status = new Status();
                            //        status.Name = row.Cell(4).Value.ToString();
                            //        status.Id = 0;
                            //        _context.Add(status);
                            //        await _context.SaveChangesAsync();
                            //        fighter.StatusId = _context.Status.Where(s => s.Name == status.Name).FirstOrDefault().Id;
                            //    }

                            //}



                            //int height;
                            //if (int.TryParse(row.Cell(5).Value.ToString(), out height))
                            //{
                            //    if (height >= 120 && height <= 250)
                            //    {
                            //        fighter.Height = height;
                            //    }
                            //}


                            //int weight;
                            //if (int.TryParse(row.Cell(6).Value.ToString(), out weight))
                            //{
                            //    if (weight >= 50 && weight <= 350)
                            //    {
                            //        fighter.Weight = weight;
                            //    }
                            //}


                            //DateTime date;
                            //if (DateTime.TryParse(row.Cell(7).Value.ToString(), out date))
                            //{
                            //    if (DateTime.Compare(date.AddYears(18), DateTime.Now) < 0)
                            //    {
                            //        fighter.DateOfBirth = date;
                            //    }
                            //}

                            //DateTime dateDebut;
                            //if (DateTime.TryParse(row.Cell(8).Value.ToString(), out dateDebut))
                            //{
                            //    if (DateTime.Compare(dateDebut, DateTime.Now) < 0)
                            //    {
                            //        if (date != null)
                            //        {
                            //            if (DateTime.Compare(date.AddYears(18), dateDebut) < 0)
                            //            {
                            //                fighter.Debut = dateDebut;
                            //            }
                            //        }
                            //        else fighter.Debut = dateDebut;


                            //    }
                            //}

                            //_context.Fighter.Add(fighter);
                            //            }
                            //            catch (Exception e)
                            //            {
                            //                Console.WriteLine("ERROR");
                            //            }
                            //        }

                            //    }
                            //}
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("ERROR");
                }
                await _context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public Microsoft.AspNetCore.Mvc.ActionResult ExportDoc(string loadReport)
        {

            Document doc = new Document();
            
            String[] Header = { "П.І.Б.", "Division", "Country", "Status", "Height", "Weight", "DateOfBirth", "Debut" };
            

            switch (loadReport)
            {
                case "All":
                    {
                        Section s = doc.AddSection();
                        
                        Paragraph par = s.AddParagraph();
                        TextRange ps = par.AppendText("All Fighters");
                        ps.CharacterFormat.FontSize = 14;
                        ps.CharacterFormat.Bold = true;
                        par.Format.HorizontalAlignment = HorizontalAlignment.Center;

                        s.AddParagraph();
                        Table table = s.AddTable(true);
                        var groupedBy = _context.Fighter.ToList();
                        //var worksheet = workbook.Worksheets.Add("Всі бійці");

                        var fighters = groupedBy;
                        string[,] data = new string[fighters.Count, 8];
                        table.ResetCells(fighters.Count + 1, Header.Length);
                        //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                        for (int i = 0; i < fighters.Count; i++)
                        {
                            String[] data2 = {
                                     fighters[i].Name,
                                     _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name,
                                     _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name,
                                     _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name,
                                     fighters[i].Height!=null?fighters[i].Height.ToString():"",
                                     fighters[i].Weight!=null?fighters[i].Weight.ToString():"",
                                     fighters[i].DateOfBirth!=null?fighters[i].DateOfBirth.Value.Date.ToString():"",
                                    fighters[i].Debut!=null?fighters[i].Debut.Value.Date.ToString():""
                                };

                            TableRow DataRow = table.Rows[i + 1];

                            //Row Height
                            DataRow.Height = 20;

                            for (int j = 0; j < data2.Length; j++)
                            {
                                data[i, j] = data2[j];
                                //Cell Alignment
                                DataRow.Cells[j].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                //Fill Data in Rows
                                Paragraph p2 = DataRow.Cells[j].AddParagraph();
                                TextRange TR2 = p2.AppendText(data[i, j]);
                                //Format Cells
                                p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                TR2.CharacterFormat.FontName = "Calibri";
                                TR2.CharacterFormat.FontSize = 9;
                            }

                        }

                        TableRow FRow = table.Rows[0];
                        FRow.IsHeader = true;
                        //Row Height
                        FRow.Height = 23;
                        //Header Format
                        for (int i = 0; i < Header.Length; i++)
                        {
                            //Cell Alignment
                            Paragraph p = FRow.Cells[i].AddParagraph();
                            FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            //Data Format
                            TextRange TR = p.AppendText(Header[i]);
                            TR.CharacterFormat.FontName = "Calibri";
                            TR.CharacterFormat.FontSize = 9;

                            TR.CharacterFormat.Bold = true;


                        }

                        break;
                    }
                case "Division":
                    {
                        var groupedBy = _context.Division.Include("Fighter").ToList();
                        //var worksheet = workbook.Worksheets.Add("Всі бійці");
                        Section s = doc.AddSection();
                        Paragraph p = s.AddParagraph();
                        
                        TextRange ps = p.AppendText("Grouped by Division");
                        ps.CharacterFormat.FontSize=14;
                        ps.CharacterFormat.Bold=true;
                        p.Format.HorizontalAlignment = HorizontalAlignment.Center;

                        s.AddParagraph();
                        foreach (var d in groupedBy)
                        {
                            
                            
                            Paragraph p3 = s.AddParagraph();
                            ps = p3.AppendText(d.Name);
                            ps.CharacterFormat.FontSize = 14;
                            ps.CharacterFormat.Bold = true;
                            p3.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            s.AddParagraph();
                            s.AddParagraph();
                            Table table = s.AddTable(true);
                            var fighters = d.Fighter.ToList();
                            string[,] data = new string[fighters.Count, 8];
                            table.ResetCells(fighters.Count + 1, Header.Length);
                            //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                            for (int i = 0; i < fighters.Count; i++)
                            {
                                String[] data2 = {
                                     fighters[i].Name,
                                     _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name,
                                     _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name,
                                     _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name,
                                     fighters[i].Height!=null?fighters[i].Height.ToString():"",
                                     fighters[i].Weight!=null?fighters[i].Weight.ToString():"",
                                     fighters[i].DateOfBirth!=null?fighters[i].DateOfBirth.Value.Date.ToString():"",
                                    fighters[i].Debut!=null?fighters[i].Debut.Value.Date.ToString():""
                                };

                                TableRow DataRow = table.Rows[i + 1];

                                //Row Height
                                DataRow.Height = 20;

                                for (int j = 0; j < data2.Length; j++)
                                {
                                    data[i, j] = data2[j];
                                    //Cell Alignment
                                    DataRow.Cells[j].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    //Fill Data in Rows
                                    Paragraph p2 = DataRow.Cells[j].AddParagraph();
                                    TextRange TR2 = p2.AppendText(data[i, j]);
                                    //Format Cells
                                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                    TR2.CharacterFormat.FontName = "Calibri";
                                    TR2.CharacterFormat.FontSize = 9;
                                }

                            }

                            TableRow FRow = table.Rows[0];
                            FRow.IsHeader = true;
                            //Row Height
                            FRow.Height = 23;
                            //Header Format
                            for (int i = 0; i < Header.Length; i++)
                            {
                                //Cell Alignment
                                Paragraph pa = FRow.Cells[i].AddParagraph();
                                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                pa.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                //Data Format
                                TextRange TR = pa.AppendText(Header[i]);
                                TR.CharacterFormat.FontName = "Calibri";
                                TR.CharacterFormat.FontSize = 9;

                                TR.CharacterFormat.Bold = true;


                            }
                            s.AddParagraph();
                        }
                        break;

                        //-----------------------
                        
                    }
                case "Country":
                    {
                        var groupedBy = _context.Country.Include("Fighter").ToList();
                        //var worksheet = workbook.Worksheets.Add("Всі бійці");
                        Section s = doc.AddSection();
                        Paragraph p = s.AddParagraph();

                        TextRange ps = p.AppendText("Grouped by Division");
                        ps.CharacterFormat.FontSize = 14;
                        ps.CharacterFormat.Bold = true;
                        p.Format.HorizontalAlignment = HorizontalAlignment.Center;

                        s.AddParagraph();
                        foreach (var d in groupedBy)
                        {


                            Paragraph p3 = s.AddParagraph();
                            ps = p3.AppendText(d.Name);
                            ps.CharacterFormat.FontSize = 14;
                            ps.CharacterFormat.Bold = true;
                            p3.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            s.AddParagraph();
                            s.AddParagraph();
                            Table table = s.AddTable(true);
                            var fighters = d.Fighter.ToList();
                            string[,] data = new string[fighters.Count, 8];
                            table.ResetCells(fighters.Count + 1, Header.Length);
                            //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                            for (int i = 0; i < fighters.Count; i++)
                            {
                                String[] data2 = {
                                     fighters[i].Name,
                                     _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name,
                                     _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name,
                                     _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name,
                                     fighters[i].Height!=null?fighters[i].Height.ToString():"",
                                     fighters[i].Weight!=null?fighters[i].Weight.ToString():"",
                                     fighters[i].DateOfBirth!=null?fighters[i].DateOfBirth.Value.Date.ToString():"",
                                    fighters[i].Debut!=null?fighters[i].Debut.Value.Date.ToString():""
                                };

                                TableRow DataRow = table.Rows[i + 1];

                                //Row Height
                                DataRow.Height = 20;

                                for (int j = 0; j < data2.Length; j++)
                                {
                                    data[i, j] = data2[j];
                                    //Cell Alignment
                                    DataRow.Cells[j].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    //Fill Data in Rows
                                    Paragraph p2 = DataRow.Cells[j].AddParagraph();
                                    TextRange TR2 = p2.AppendText(data[i, j]);
                                    //Format Cells
                                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                    TR2.CharacterFormat.FontName = "Calibri";
                                    TR2.CharacterFormat.FontSize = 9;
                                }

                            }

                            TableRow FRow = table.Rows[0];
                            FRow.IsHeader = true;
                            //Row Height
                            FRow.Height = 23;
                            //Header Format
                            for (int i = 0; i < Header.Length; i++)
                            {
                                //Cell Alignment
                                Paragraph pa = FRow.Cells[i].AddParagraph();
                                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                pa.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                //Data Format
                                TextRange TR = pa.AppendText(Header[i]);
                                TR.CharacterFormat.FontName = "Calibri";
                                TR.CharacterFormat.FontSize = 9;

                                TR.CharacterFormat.Bold = true;


                            }
                            s.AddParagraph();
                        }
                        break;

                        //-----------------------
                    }
                case "Status":
                    {
                        var groupedBy = _context.Status.Include("Fighter").ToList();
                        //var worksheet = workbook.Worksheets.Add("Всі бійці");
                        Section s = doc.AddSection();
                        Paragraph p = s.AddParagraph();

                        TextRange ps = p.AppendText("Grouped by Division");
                        ps.CharacterFormat.FontSize = 14;
                        ps.CharacterFormat.Bold = true;
                        p.Format.HorizontalAlignment = HorizontalAlignment.Center;

                        s.AddParagraph();
                        foreach (var d in groupedBy)
                        {


                            Paragraph p3 = s.AddParagraph();
                            ps = p3.AppendText(d.Name);
                            ps.CharacterFormat.FontSize = 14;
                            ps.CharacterFormat.Bold = true;
                            p3.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            s.AddParagraph();
                            s.AddParagraph();
                            Table table = s.AddTable(true);
                            var fighters = d.Fighter.ToList();
                            string[,] data = new string[fighters.Count, 8];
                            table.ResetCells(fighters.Count + 1, Header.Length);
                            //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                            for (int i = 0; i < fighters.Count; i++)
                            {
                                String[] data2 = {
                                     fighters[i].Name,
                                     _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name,
                                     _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name,
                                     _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name,
                                     fighters[i].Height!=null?fighters[i].Height.ToString():"",
                                     fighters[i].Weight!=null?fighters[i].Weight.ToString():"",
                                     fighters[i].DateOfBirth!=null?fighters[i].DateOfBirth.Value.Date.ToString():"",
                                    fighters[i].Debut!=null?fighters[i].Debut.Value.Date.ToString():""
                                };

                                TableRow DataRow = table.Rows[i + 1];

                                //Row Height
                                DataRow.Height = 20;

                                for (int j = 0; j < data2.Length; j++)
                                {
                                    data[i, j] = data2[j];
                                    //Cell Alignment
                                    DataRow.Cells[j].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    //Fill Data in Rows
                                    Paragraph p2 = DataRow.Cells[j].AddParagraph();
                                    TextRange TR2 = p2.AppendText(data[i, j]);
                                    //Format Cells
                                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                    TR2.CharacterFormat.FontName = "Calibri";
                                    TR2.CharacterFormat.FontSize = 9;
                                }

                            }

                            TableRow FRow = table.Rows[0];
                            FRow.IsHeader = true;
                            //Row Height
                            FRow.Height = 23;
                            //Header Format
                            for (int i = 0; i < Header.Length; i++)
                            {
                                //Cell Alignment
                                Paragraph pa = FRow.Cells[i].AddParagraph();
                                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                pa.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                //Data Format
                                TextRange TR = pa.AppendText(Header[i]);
                                TR.CharacterFormat.FontName = "Calibri";
                                TR.CharacterFormat.FontSize = 9;

                                TR.CharacterFormat.Bold = true;


                            }
                            s.AddParagraph();
                        }
                        break;

                        //-----------------------
                    }
                default:
                    {
                        Section s = doc.AddSection();
                        
                        Paragraph par = s.AddParagraph();
                        par.AppendText("All fighters");
                        Table table = s.AddTable(true);
                        var groupedBy = _context.Fighter.ToList();
                        //var worksheet = workbook.Worksheets.Add("Всі бійці");

                        var fighters = groupedBy;
                        string[,] data = new string[fighters.Count, 8];
                        table.ResetCells(fighters.Count + 1, Header.Length);
                        //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                        for (int i = 0; i < fighters.Count; i++)
                        {
                            String[] data2 = {
                                     fighters[i].Name,
                                     _context.Division.Where(d => d.Id == fighters[i].DivisionId).FirstOrDefault().Name,
                                     _context.Country.Where(c => c.Id == fighters[i].CountryId).FirstOrDefault().Name,
                                     _context.Status.Where(s => s.Id == fighters[i].StatusId).FirstOrDefault().Name,
                                     fighters[i].Height!=null?fighters[i].Height.ToString():"",
                                     fighters[i].Weight!=null?fighters[i].Weight.ToString():"",
                                     fighters[i].DateOfBirth!=null?fighters[i].DateOfBirth.Value.Date.ToString():"",
                                    fighters[i].Debut!=null?fighters[i].Debut.Value.Date.ToString():""
                                };

                            TableRow DataRow = table.Rows[i + 1];

                            //Row Height
                            DataRow.Height = 20;

                            for (int j = 0; j < data2.Length; j++)
                            {
                                data[i, j] = data2[j];
                                //Cell Alignment
                                DataRow.Cells[j].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                //Fill Data in Rows
                                Paragraph p2 = DataRow.Cells[j].AddParagraph();
                                TextRange TR2 = p2.AppendText(data[i, j]);
                                //Format Cells
                                p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                TR2.CharacterFormat.FontName = "Calibri";
                                TR2.CharacterFormat.FontSize = 9;
                            }

                        }

                        TableRow FRow = table.Rows[0];
                        FRow.IsHeader = true;
                        //Row Height
                        FRow.Height = 23;
                        //Header Format
                        for (int i = 0; i < Header.Length; i++)
                        {
                            //Cell Alignment
                            Paragraph p = FRow.Cells[i].AddParagraph();
                            FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            //Data Format
                            TextRange TR = p.AppendText(Header[i]);
                            TR.CharacterFormat.FontName = "Calibri";
                            TR.CharacterFormat.FontSize = 9;

                            TR.CharacterFormat.Bold = true;


                        }

                        break;
                    }
            }




            using (var stream = new MemoryStream())
            {
                doc.SaveToStream(stream, FileFormat.Docx);
                stream.Flush();
                return new Microsoft.AspNetCore.Mvc.FileContentResult(stream.ToArray(),
                  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"fighters_{ DateTime.UtcNow.ToShortDateString()}.docx"
                };
            }

        }
    
    }
}
