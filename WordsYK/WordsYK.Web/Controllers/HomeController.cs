using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;

namespace WordsYK.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var modes = new List<Mode>
            {
            new Mode { Name = "Random10", Value=10 },
            new Mode { Name = "Random20", Value=20 },
            new Mode { Name = "Random50", Value=50 },
            };

            return View(modes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
