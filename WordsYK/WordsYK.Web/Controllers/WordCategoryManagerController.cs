using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;
using System.IO;
using DataInMemory;

namespace WordsYK.WebUI.Controllers
{
    public class WordCategoryManagerController : Controller
    {
        WordCategoryRepository context;

        public WordCategoryManagerController()
        {
            context = new WordCategoryRepository();
        }

        public ActionResult Index()
        {
            List<WordCategory> wordCategories = context.Collection().ToList();
            return View(wordCategories);
        }

        public ActionResult Create()
        {
            var wordCategory = new WordCategory();
            return View(wordCategory);
        }

        [HttpPost]
        public ActionResult Create(WordCategory wordCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(wordCategory);
            }
            else
            {
                context.Insert(wordCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            WordCategory wordCategory = context.Find(Id);
            if (wordCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(wordCategory);
            }

        }

        [HttpPost]
        public ActionResult Edit(WordCategory wordCategory, string Id, HttpPostedFileBase file)
        {
            WordCategory wordCategoryToEdit = context.Find(Id);

            if (wordCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(wordCategory);
                }

                wordCategoryToEdit.Category = wordCategory.Category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            WordCategory wordToDelete = context.Find(Id);

            if (wordToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(wordToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            WordCategory wordCategoryToDelete = context.Find(Id);

            if (wordCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}