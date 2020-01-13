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

        public ActionResult Index(string Category=null, int WordsNumber=10)
        {

            var NumberOfWordsInput = WordsNumber;
            // So far jsut declaring an int variable NumberOfWordsInput to late use in the model.

            List<WordCategory> categoriesToInclude;

            if (Category == null)
            {
                categoriesToInclude = wordCategoriesContext.Collection().ToList();
            }
            else
            {
                categoriesToInclude = wordCategoriesContext.Collection().Where(cat => cat.Category == Category).ToList();
            }



            var model = new ModeViewModel();
            model.WordCategoryTypes = categoriesToInclude;
            model.NumberOfWords = WordsNumber;
            // To give the option to enter number of words later. Right now we use Default value of 10.




            return View(model);
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
