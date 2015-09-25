using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuellerThomasMKN_151.Models
{
    public class Game
    {
        protected static int MAX_ERRORS_EASY = 10;
        protected static int MAX_ERRORS_MEDIUM = 7;
        protected static int MAX_ERRORS_HARD = 5;

        public enum eGameState { ACTIVE, WON, LOST }

        public int gameID { get; set; }
        public int maxErrors { get; set; }
        public int currentErrors { get; set; }
        public string completeWord { get; set; } // solution
        public string hiddenWord { get; set; }   // Underlines for letters which aren't found yet
        public string usedLetters { get; set; }

        public eGameState state { get; set; }


        public Game (int gameID) {
            gameID          = -1;
            currentErrors   = 0;
            maxErrors       = MAX_ERRORS_MEDIUM;
            completeWord    = "";
            hiddenWord      = "";
            usedLetters     = "";
            state           = eGameState.ACTIVE;
        }

        // replaces every character in completeWord with a '_'
        public static string CreateHiddenWord(string completeWord)
        {
            return new string('_', completeWord.Length);
        }

        // Insert a space between every character
        public static string InsertSpaces(string word)
        {
            return String.Join<char>(" ", word);
        }

        public void LetterChosen(char letter) {
            // check if letter iscorrect 
            if (IsLetterCorrect(letter))
            {
                // replace underscores in hiddenword with the letter
                FillInLetter(letter);
            } else
            {
                currentErrors++;
            }
            usedLetters += letter;
            state = GetGameState();

        }

        protected bool IsLetterCorrect(char letter)
        {
            return completeWord.ToUpper().Contains(letter) && !hiddenWord.ToUpper().Contains(letter);
        }


        protected void FillInLetter(char letter)
        {
            StringBuilder word = new StringBuilder(hiddenWord);
            for (int pos = pos = completeWord.ToUpper().IndexOf(letter); pos != -1 && pos < completeWord.Length && pos < word.Length; pos++, pos = completeWord.ToUpper().IndexOf(letter, pos))
            {
                if (pos >= 0 && pos < word.Length)
                {
                    word[pos] = completeWord[pos];
                }
            }
            hiddenWord = word.ToString();
        }


        protected eGameState GetGameState()
        {
            if (currentErrors >= maxErrors)
            {
                return eGameState.LOST;
            }
            else if (completeWord.ToUpper().Equals(hiddenWord.ToUpper()))
            {
                return eGameState.WON;
            }
            return eGameState.ACTIVE;
        }


        public string isDisabled(char letter)
        {
            if (usedLetters.Contains(letter)) {
                return "disabled";
            }
            return "";            
        }
    }
}
