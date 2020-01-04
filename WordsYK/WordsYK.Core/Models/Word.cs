using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsYK.Core.Models
{
    public class Word : Base
    {
        [StringLength(100)]
        public string EnTranslation { get; set; }
        public string SeTranslation { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

    }
}
