using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly FightContext _context;

        public ChartsController(FightContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]

        public JsonResult JsonData(string param)
        {
            switch (param)
            {
                case "Division":
                    {
                        var division = _context.Division.ToList();
                        List<object> divisFighters = new List<object>();
                        divisFighters.Add(new[] { "Вагова категорія", "К-сть бійців" });

                        foreach (var d in division)
                        {
                            divisFighters.Add(new object[] { d.Name, _context.Fighter.Where(f => f.DivisionId == d.Id).ToList().Count() });
                        }
                        return new JsonResult(divisFighters);
                    }
                case "Country":
                    {
                        var country = _context.Country.ToList();
                        List<object> countrFighters = new List<object>();
                        countrFighters.Add(new[] { "Країна", "К-сть бійців" });

                        foreach (var c in country)
                        {
                            countrFighters.Add(new object[] { c.Name, _context.Fighter.Where(f => f.CountryId == c.Id).ToList().Count() });
                        }
                        return new JsonResult(countrFighters);
                    }
                case "Status":
                    {
                        var status = _context.Status.ToList();
                        List<object> statFighters = new List<object>();
                        statFighters.Add(new[] { "Статус", "К-сть бійців" });

                        foreach (var s in status)
                        {
                            statFighters.Add(new object[] { s.Name, _context.Fighter.Where(f => f.StatusId == s.Id).ToList().Count() });
                        }
                        return new JsonResult(statFighters);
                    }
                default:
                    {
                        var division = _context.Division.ToList();
                        List<object> divisFighters = new List<object>();
                        divisFighters.Add(new[] { "Вагова категорія", "К-сть бійців" });

                        foreach (var d in division)
                        {
                            divisFighters.Add(new object[] { d.Name, _context.Fighter.Where(f => f.DivisionId == d.Id).ToList().Count() });
                        }
                        return new JsonResult(divisFighters);
                    }
            }
            
        }
    }
}