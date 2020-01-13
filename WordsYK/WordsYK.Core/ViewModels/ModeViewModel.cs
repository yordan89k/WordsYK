using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsYK.Core.Models;

namespace WordsYK.Core.ViewModels
{
    public class ModeViewModel
    {
        public int NumberOfWords { get; set; }

        public IEnumerable<Word> Words { get; set; }

        public IEnumerable<WordCategory> WordCategories { get; set; }
    }
}
