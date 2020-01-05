using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using WordsYK.Core.Models;

namespace DataInMemory
{
    public class WordCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<WordCategory> wordCategories;

        public WordCategoryRepository()
        {
            wordCategories = cache["wordCategories"] as List<WordCategory>;
            if (wordCategories == null)
            {
                new List<WordCategory>();
            }
        }

        public void Commit()
        {
            cache["wordCategories"] = wordCategories;
        }

        public void Insert(WordCategory wcat)
        {
            wordCategories.Add(wcat);
        }

        public void Update(WordCategory wordCategory)
        {
            WordCategory wordCategoryToUpdate = wordCategories.Find(wcat => wcat.Id == wordCategory.Id);

            if (wordCategoryToUpdate != null)
            {
                wordCategoryToUpdate = wordCategory;
            }
            else
            {
                throw new Exception("Word Category not found");
            }
        }

        public WordCategory Find(string Id)
        {
            WordCategory wordCategory = wordCategories.Find(wcat => wcat.Id == Id);

            if (wordCategory != null)
            {
                return wordCategory;
            }
            else
            {
                throw new Exception("Word Category not found");
            }
        }

        public IQueryable<WordCategory> Collection()
        {
            return wordCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            WordCategory wordCategoryToDelete = wordCategories.Find(wcat => wcat.Id == Id);

            if (wordCategoryToDelete != null)
            {
                wordCategories.Remove(wordCategoryToDelete);
            }
            else
            {
                throw new Exception("Word Category not found");
            }
        }


    }

}

