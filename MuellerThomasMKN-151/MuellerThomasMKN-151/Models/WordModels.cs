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
        public String Name { get; set; }
    }
}
