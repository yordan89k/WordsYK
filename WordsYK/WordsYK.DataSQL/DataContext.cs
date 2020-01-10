using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsYK.Core.Models;

namespace WordsYK.DataSQL
{
    public class DataContext : DbContext
    {
        public DataContext()
             : base("DefaultConnection")
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<WordCategory> WordCategories { get; set; }

    }
}
