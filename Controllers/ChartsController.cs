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

        public JsonResult JsonData()
        {
            var division = _context.Division.ToList();
            List<object> divisFighters = new List<object>();
            divisFighters.Add(new[] { "Вагова категорія", "К-сть бійців" });

            foreach(var d in division)
            {
                divisFighters.Add(new object[] { d.Name, _context.Fighter.Where(f=>f.DivisionId==d.Id).ToList().Count() });
            }
            return new JsonResult(divisFighters);
        }

        [HttpGet("JsonData2")]

        public JsonResult JsonData2()
        {
            var countries = _context.Country.ToList();
            List<object> countryFighters = new List<object>();
            countryFighters.Add(new[] { "Країна", "К-сть бійців" });

            foreach (var c in countries)
            {
                countryFighters.Add(new object[] { c.Name, _context.Fighter.Where(f => f.CountryId == c.Id).ToList().Count() });
            }
            return new JsonResult(countryFighters);
        }
    }
}