using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuellerThomasMKN_151.Models
{
    public class Game
    {
        public static int MAX_ERRORS = 5;
        public enum eGameState { ACTIVE, WON, LOST }

        public int gameID { get; set; }
        public int currentErrors { get; set; }
        public string completeWord { get; set; } // solution
        public string hiddenWord { get; set; }   // Underlines for letters which aren't found yet

        public eGameState state { get; set; }


        public Game (int gameID) {
            gameID          = -1;
            currentErrors   = 0;
            completeWord    = "";
            hiddenWord      = "";
            state           = eGameState.ACTIVE;
        }
    }
}
