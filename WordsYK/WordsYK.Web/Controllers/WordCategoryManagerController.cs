using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsYK.Core.Models;
using System.IO;
using MyShopYK.DataAccess.InMemory;
using MyShopYK.Core.Contracts;

namespace WordsYK.WebUI.Controllers
{
    public class WordCategoryManagerController : Controller
    {
            IRepository<WordCategory> wordCategoriesContext;

            public WordCategoryManagerController(IRepository<WordCategory> wordCategoriesContext)
            {
            this.wordCategoriesContext = wordCategoriesContext;
            }


            public ActionResult Index()
            {
                List<WordCategory> wordCategories = wordCategoriesContext.Collection().ToList();
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
                    wordCategoriesContext.Insert(wordCategory);
                    wordCategoriesContext.Commit();

                    return RedirectToAction("Index");
                }
            }

            public ActionResult Edit(string Id)
            {
                WordCategory wordCategory = wordCategoriesContext.Find(Id);
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
                WordCategory wordCategoryToEdit = wordCategoriesContext.Find(Id);

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

                    wordCategoriesContext.Commit();

                    return RedirectToAction("Index");
                }
            }

            public ActionResult Delete(string Id)
            {
                WordCategory wordToDelete = wordCategoriesContext.Find(Id);

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
                WordCategory wordCategoryToDelete = wordCategoriesContext.Find(Id);

                if (wordCategoryToDelete == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    wordCategoriesContext.Delete(Id);
                    wordCategoriesContext.Commit();

                    return RedirectToAction("Index");
                }
            }
        }
    }