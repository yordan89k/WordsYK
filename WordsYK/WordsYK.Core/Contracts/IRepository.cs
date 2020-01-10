using System.Linq;
using WordsYK.Core.Models;

namespace MyShopYK.Core.Contracts
{
    public interface IRepository<T> where T : Base
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}