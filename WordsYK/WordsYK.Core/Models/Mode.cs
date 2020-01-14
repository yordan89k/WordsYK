using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsYK.Core.Models
{
    public class Mode : Base
    {
        public string Name { get; set; }
        public int NumberOfWords { get; set; }
        public List<string> WordCategoryTypes { get; set; }
        //Not sure if the variable can be list here. Will try.

        public List<Word> WordsToInclude { get; set; }
    }

}
