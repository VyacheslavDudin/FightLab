using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;

using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace WebApplication2.Controllers
{
    
    public class RemoteValidationController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly FightContext _context;

        
        public RemoteValidationController(FightContext context)
        {
            _context = context;
        }
        [System.Web.Mvc.HttpPost]
        public JsonResult NameExist(string Name)
        {

            if (_context.Division.Where(d => d.Name == Name).Count()!=0)
            {
                 return Json("NAME EXIST",
                        JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}