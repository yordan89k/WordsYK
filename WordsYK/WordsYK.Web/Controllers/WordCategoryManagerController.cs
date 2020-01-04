using MyShopYK.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;

namespace WordsYK.Web.Controllers
{
    public class WordCategoryManagerController : Controller
    {
        InMemoryRepository<WordCategory> context;

        public WordCategoryManagerController()
        {
            context = new InMemoryRepository<WordCategory>();
        }
    }
}