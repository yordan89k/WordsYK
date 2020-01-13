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

        public ActionResult Index(List<String> CategoriesToInclude = null, int WordsNumber=10)
        {
            List<Word> wordsToInclude;
            List<WordCategory> allWordCategories;

            wordsToInclude = null;
            allWordCategories = wordCategoriesContext.Collection().ToList();

            var NumberOfWordsInput = WordsNumber;
            // So far just declaring an int variable NumberOfWordsInput to late use in the model.

            if (CategoriesToInclude == null)
            {
                wordsToInclude = wordContext.Collection().ToList();
            }
            else
            {
                foreach (var category in CategoriesToInclude)
                {
                    wordsToInclude = wordContext.Collection().Where(w => w.Category == category).ToList();
                }
            }
            // Created a list with categories to include. Can work with it later.


            

            //wordsToInclude = wordContext.Collection().Where(w => w.Category == category).ToList();






            var model = new ModeViewModel();
            model.WordCategories = allWordCategories;
            model.NumberOfWords = WordsNumber;
            model.Words = wordsToInclude;
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
