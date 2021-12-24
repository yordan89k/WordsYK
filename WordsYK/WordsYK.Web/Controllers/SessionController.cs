using MyShopYK.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;

namespace WordsYK.Web.Controllers
{
    public class SessionController : Controller
    {
        IRepository<Word> wordContext;
        IRepository<WordCategory> wordCategoriesContext;

        public SessionController(IRepository<Word> wordContext, IRepository<WordCategory> wordCategoriesContext)
        {
            this.wordContext = wordContext;
            this.wordCategoriesContext = wordCategoriesContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WordSession()
        {
            var mode = new Mode();
            var wordsNumber = !string.IsNullOrEmpty(Request.Form["wordsnumberinput"]) ? Convert.ToInt32(Request.Form["wordsnumberinput"]) : 10;
            var categoriesToInclude = Request.Form["categoriesinput"] != null ? Request.Form["categoriesinput"].Split(',').ToList() : new List<string>() { };

            mode.NumberOfWords = wordsNumber;
            mode.WordCategoryTypes = categoriesToInclude;
            mode.WordsToInclude = RandomizeItemOrderInList(BuildWordsToIncludeList(categoriesToInclude));
            mode.Name = "Session of " + mode.NumberOfWords.ToString() + (mode.NumberOfWords > 1 ? " words " : " word ") 
                +  "of type " + Request.Form["categoriesinput"];

            return View(mode);
        }

        private List<Word> BuildWordsToIncludeList(List<string> categoriesToInclude)
        {
            var result = new List<Word>();
            if (categoriesToInclude == null || !categoriesToInclude.Any())
                return result;

            foreach (var category in categoriesToInclude)
            {
                var WordsTemp = wordContext.Collection().Where(w => w.Category == category).ToList();
                if (WordsTemp != null)
                {
                    foreach (var word in WordsTemp)
                    {
                        result.Add(word);
                    }
                }
            }
            return result;
        }

        private List<Word> RandomizeItemOrderInList(List<Word> listToBeRandomized)
        {
            if (listToBeRandomized == null || listToBeRandomized.Count <= 1)
                return listToBeRandomized;

            var n = listToBeRandomized.Count;
            while (n > 1)
            {
                n--;
                int k = new Random().Next(n + 1);
                Word value = listToBeRandomized[k];
                listToBeRandomized[k] = listToBeRandomized[n];
                listToBeRandomized[n] = value;
            }
            return listToBeRandomized;
        }
    }
}