using MyShopYK.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;

namespace WordsYK.Web.Controllers
{
    public class WordManagerController : Controller
    {
        InMemoryRepository<Word> context;
        InMemoryRepository<WordCategory> wordCategories;

        public WordManagerController()
        {
            context = new InMemoryRepository<Word>();
            wordCategories = new InMemoryRepository<WordCategory>();
        }
    }
}
