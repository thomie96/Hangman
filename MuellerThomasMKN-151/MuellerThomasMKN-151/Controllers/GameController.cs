using MuellerThomasMKN_151.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MuellerThomasMKN_151.Controllers
{
    public class GameController : Controller
    {
        protected ApplicationDbContext db = ApplicationDbContext.Create();

        protected Game gameData = new Game (1);

        // GET: Game
        public ActionResult Index()
        {
            gameData.completeWord   = GetRandomWord();
            gameData.hiddenWord     = CreateHiddenWord (gameData.completeWord);
            ViewData ["Word"]       = InsertSpaces(gameData.hiddenWord);

            Session.Add("Hangman", gameData);
            return View();
        }

        
        protected string GetRandomWord()
        {
            var Words = db.Words.ToList();

            Random rand = new Random();
            int rnd = rand.Next(0, Words.Count() - 1);
            return Words[rnd].Name;

        }

        // replaces every character in completeWord with a '_'
        public string CreateHiddenWord (string completeWord)
        {
            return new string ('_', completeWord.Length);
        }


        // Insert a space between every character
        public string InsertSpaces(string word)
        {
            return String.Join<char>(" ", word);
        }

        [HttpPost]
        public ActionResult LetterChosen(string value)
        {
            // error input ---- shouldn't be possible
            if (value == null || value.Length == 0) {
                return View();
            }

            char letter = value[0];
            // check if letter iscorrect 
            if (IsLetterCorrect(letter)) {
                // replace underscores in hiddenword with the letter
                FillInLetter(letter);
            }

            return View();
        }


        protected bool IsLetterCorrect(char letter)
        {
            return gameData.completeWord.Contains(letter) && !gameData.hiddenWord.Contains(letter);
        }


        protected void FillInLetter(char letter)
        {
            StringBuilder word = new StringBuilder(gameData.hiddenWord); 
            for (int pos = pos = gameData.completeWord.IndexOf(letter); pos < gameData.completeWord.Length && pos < word.Length; pos++, pos = gameData.completeWord.IndexOf(letter, pos)) {
                if (pos >= 0 && pos < word.Length) {
                    word[pos] = letter;
                }
            }
            gameData.hiddenWord = word.ToString();
        }


        protected Game.eGameState GetGameState()
        {
            if (gameData.currentErrors >= Game.MAX_ERRORS)
            {
                return Game.eGameState.LOST;
            }
            else if (gameData.completeWord.Equals(gameData.hiddenWord))
            {
                return Game.eGameState.WON;
            }
            return Game.eGameState.ACTIVE;
        }

    }
    
}