using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;
using System.IO;
using WordsYK.Core.ViewModels;
using MyShopYK.DataAccess.InMemory;
using MyShopYK.Core.Contracts;

namespace WordsYK.WebUI.Controllers
{
    public class WordManagerController : Controller
    {
        IRepository<Word> wordContext;
        IRepository<WordCategory> wordCategoriesContext;

        public WordManagerController(IRepository<Word> wordContext, IRepository<WordCategory> wordCategoriesContext)
        {
            this.wordContext = wordContext;
            this.wordCategoriesContext = wordCategoriesContext;
        }



        //Defining the modes here as a start. Could find better practice in future.
        public ActionResult Index()
        {
            List<Word> words = wordContext.Collection().ToList();
            return View(words);
        }

        public ActionResult Create()
        {
            var viewModel = new WordCategoryViewModel();
            viewModel.Word = new Word();
            viewModel.WordCategories = wordCategoriesContext.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Word word, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(word);
            }
            else
            {
                if (file != null)
                {
                    word.Image = word.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//WordImages//") + word.Image);
                }

                wordContext.Insert(word);
                wordContext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Word word = wordContext.Find(Id);
            if (word == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = new WordCategoryViewModel();
                viewModel.Word = word;
                viewModel.WordCategories = wordCategoriesContext.Collection();
                return View(viewModel);
            }

        }

        [HttpPost]
        public ActionResult Edit(Word word, string Id, HttpPostedFileBase file)
        {
            Word wordToEdit = wordContext.Find(Id);

            if (wordToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(word);
                }

                if (file != null)
                {
                    wordToEdit.Image = word.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//WordImages//") + wordToEdit.Image);
                }

                wordToEdit.EnTranslation = word.EnTranslation;
                wordToEdit.SeTranslation = word.SeTranslation;
                wordToEdit.Category = word.Category;

                wordContext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Word wordToDelete = wordContext.Find(Id);

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
            Word wordToDelete = wordContext.Find(Id);

            if (wordToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                wordContext.Delete(Id);
                wordContext.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}
