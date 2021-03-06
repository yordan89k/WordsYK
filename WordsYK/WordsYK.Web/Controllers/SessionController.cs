﻿using MyShopYK.Core.Contracts;
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
        // GET: Session

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

        public ActionResult WordSession(List<String> CategoriesToInclude = null, int WordsNumber = 10)
        {

            var mode = new Mode();
            var WordsTemp = new List<Word>(); 
            var rnd = new Random();

            WordsNumber = System.Convert.ToInt32(Request.Form["wordsnumberinput"]);
            mode.NumberOfWords = WordsNumber;


            CategoriesToInclude = Request.Form["categoriesinput"].Split(',').ToList();
            // -! To do some defensive programming here. Can't split if input is Null.
            mode.WordCategoryTypes = CategoriesToInclude;
            mode.WordsToInclude = new List<Word>();

            // -! Not the clear code. To optimize it later since it has 2 ifs and 2 foreach in each other
            if (CategoriesToInclude == null)
            {
                mode.WordsToInclude = wordContext.Collection().ToList();
            }
            else
            {
                foreach (var category in CategoriesToInclude)
                {
                    WordsTemp = wordContext.Collection().Where(w => w.Category == category).ToList();
                    if (WordsTemp != null)
                    {

                        foreach (var word in WordsTemp)
                        {
                            mode.WordsToInclude.Add(word);
                        }
                    }
                }
            }

            mode.Name = string.Format("Session of {0} words of type {1}", mode.NumberOfWords, Request.Form["categoriesinput"]);
            // Now we have the categories in a list and the words in a list



        
            int n = mode.WordsToInclude.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Word value = mode.WordsToInclude[k];
                mode.WordsToInclude[k] = mode.WordsToInclude[n];
                mode.WordsToInclude[n] = value;
            }
            //Now the words in the list are in random order


            return View(mode);

        }     
    }
}