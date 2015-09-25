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
        public string showWord { get; set; }     // word that is shown; it contains spaces between each letter
        public string usedLetters { get; set; }

        public eGameState state { get; set; }


        public Game (int gameID) {
            this.gameID          = gameID;
            this.currentErrors   = 0;
            this.maxErrors       = MAX_ERRORS_EASY;
            this.completeWord    = "";
            this.hiddenWord      = "";
            this.showWord        = "";
            this.usedLetters     = "";
            this.state           = eGameState.ACTIVE;
        }

        // initialize the words
        public void InitializeWords(string word) {
            completeWord    = word;
            hiddenWord      = CreateHiddenWord(completeWord);
            showWord        = InsertSpaces(hiddenWord);
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


        // called when letter is chosen
        public void LetterChosen(char letter) {
            // check if letter iscorrect 
            if (IsLetterCorrect(letter)) {
                // replace underscores in hiddenword with the letter
                FillInLetter(letter);
                // put spaces between letters
                showWord = InsertSpaces(hiddenWord);
            } else {
                currentErrors++;
            }
            // add the clicked letter to the used ones
            usedLetters += letter;
            // load the new State
            state = GetGameState();
        }

        // returns if letter is contained in the word but not in the hiddenword
        protected bool IsLetterCorrect(char letter)
        {
            return completeWord.ToUpper().Contains(letter) && !hiddenWord.ToUpper().Contains(letter);
        }

        // replace underscore in hiddenword with the letter
        protected void FillInLetter(char letter)
        {
            StringBuilder word = new StringBuilder(hiddenWord);
            int pos = completeWord.ToUpper().IndexOf(letter);

            for (; pos != -1 && pos < completeWord.Length && pos < word.Length; 
                pos = completeWord.ToUpper().IndexOf(letter, ++pos))
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

        // check if 
        public string isUsedLetter(char letter)
        {
            if (usedLetters.Contains(letter)) {
                return "disabled";
            }
            return "";            
        }
    }
}
