using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuellerThomasMKN_151.Models
{
    class Game
    {
        public static int MAX_ERRORS = 5;

        public int gameID { get; set; }
        public int currentErrors { get; set; }
        public string completeWord { get; set; }
        public string showWord { get; set; }

        public Game (int gameID) {
            gameID          = -1;
            currentErrors   = 0;
            completeWord    = "";
            showWord        = "";
        }
    }
}
