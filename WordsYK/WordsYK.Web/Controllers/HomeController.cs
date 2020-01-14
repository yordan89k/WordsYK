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

        public ActionResult SetMode(List<String> CategoriesToInclude = null, int WordsNumber = 10)
        {

            var mode = new Mode();


            WordsNumber = System.Convert.ToInt32(Request.Form["words1"]); 
            mode.NumberOfWords = WordsNumber;


            CategoriesToInclude = Request.Form["categories1"].Split(',').ToList();
            mode.WordCategoryTypes = CategoriesToInclude;

            if (CategoriesToInclude == null)
            {
                mode.WordsToInclude = wordContext.Collection().ToList();
            }
            else
            {
                foreach (var category in CategoriesToInclude)
                {
                    mode.WordsToInclude = wordContext.Collection().Where(w => w.Category == category).ToList();
                }
            }

            return View(mode);
        }


        /* OLD INDEX !!
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
             // Some of the logic here should be in another controller. Here we need to show all Categories and number of words.
             // Then in another controller we need to create list/collection of X? random words from X? categories (as a start)  

             return View(model);
         }

     */

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
