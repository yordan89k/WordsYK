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
    public class WordManagerController : Controller
    {
        WordRepository context;

        public WordManagerController()
        {
            context = new WordRepository();
        }

        //Defining the modes here as a start. Could find better practice in future.
        public ActionResult Index()
        {
            List<Word> words = context.Collection().ToList();
            return View(words);
        }

        public ActionResult Create()
        {
            var word = new Word();
            return View(word);
        }

        [HttpPost]
        public ActionResult Create(Word word)
        {
            if (!ModelState.IsValid)
            {
                return View(word);
            }
            else
            {
                context.Insert(word);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Word word = context.Find(Id);
            if (word == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(word);
            }

        }

        [HttpPost]
        public ActionResult Edit(Word word, string Id, HttpPostedFileBase file)
        {
            Word wordToEdit = context.Find(Id);

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
                wordToEdit.Image = word.Image;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Word wordToDelete = context.Find(Id);

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
            Word wordToDelete = context.Find(Id);

            if (wordToDelete == null)
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
