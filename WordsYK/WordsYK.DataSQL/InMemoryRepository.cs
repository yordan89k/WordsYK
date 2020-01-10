using WordsYK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShopYK.Core.Contracts;

namespace MyShopYK.DataAccess.InMemory

{
    public class InMemoryRepository<T> : IRepository<T> where T : Base
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> Items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            Items = cache[className] as List<T>;
            if (Items == null)
            {
                Items = new List<T>();
            }

        }

        public void Commit()
        {
            cache[className] = Items;
        }

        public void Insert(T t)
        {
            Items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = Items.Find(i => i.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public T Find(string Id)
        {
            T t = Items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return Items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = Items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                Items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

    }

}