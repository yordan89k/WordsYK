using MyShopYK.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;
using WordsYK.Core.ViewModels;

namespace WordsYK.Web.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Word> wordContext;
        IRepository<WordCategory> wordCategoriesContext;

        public HomeController(IRepository<Word> wordContext, IRepository<WordCategory> wordCategoriesContext)
        {
            this.wordContext = wordContext;
            this.wordCategoriesContext = wordCategoriesContext;
        }


        public ActionResult Index()
        {
            List<WordCategory> allWordCategories;
            allWordCategories = wordCategoriesContext.Collection().ToList();

            var model = new HomePageViewModel();
            model.WordCategories = allWordCategories;
            model.NumberOfWords = 10;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Believe it or not, this is a description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
