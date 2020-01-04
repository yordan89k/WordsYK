using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using WordsYK.Core.Models;

namespace DataInMemory
{
    public class WordRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Word> words;

        public WordRepository()
        {
            words = cache["words"] as List<Word>;
            if (words == null)
            {
                new List<Word>();
            }
        }

        public void Commit()
        {
            cache["words"] = words;
        }

        public void Insert(Word w)
        {
            words.Add(w);
        }

        public void Update(Word word)
        {
            Word wordToUpdate = words.Find(w => w.Id == word.Id);

            if (wordToUpdate != null)
            {
                wordToUpdate = word;
            }
            else 
            {
                throw new Exception("Word not found");
            }
        }

        public Word Find(string Id)
        {
            Word word = words.Find(w => w.Id == Id);

            if (word != null)
            {
                return word;
            }
            else
            {
                throw new Exception("Word not found");
            }
        }

        public IQueryable<Word> Collection()
        {
            return words.AsQueryable();
        }

        public void Delete(string Id)
        {
            Word wordToDelete = words.Find(w => w.Id == Id);

            if (wordToDelete != null)
            {
                words.Remove(wordToDelete);
            }
            else
            {
                throw new Exception("Word not found");
            }
        }


    }

}

