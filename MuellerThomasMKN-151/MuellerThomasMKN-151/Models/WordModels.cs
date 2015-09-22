using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuellerThomasMKN_151.Models
{
    public class Word
    {
        public int ID { get; set; }
        [Display (Name = "Wort")]
        [RegularExpression("([A-Za-zöäü]*)", ErrorMessage = "Im Wort dürfen nur Buchstaben enthalten sein.")]
        [StringLength (15, ErrorMessage = "Das {0} muss zwischen {2} und {1} Buchstaben lang sein.", MinimumLength = 4)]
        public String Name { get; set; }
    }
}
