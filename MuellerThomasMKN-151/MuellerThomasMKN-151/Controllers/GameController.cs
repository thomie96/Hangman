using MuellerThomasMKN_151.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MuellerThomasMKN_151.Controllers
{
    public class GameController : Controller
    {
        protected ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: Game
        public ActionResult Index() {
            Game game = new Game(1);
            game.InitializeWords (GetRandomWord());
            // Add Game to a session
            Session.Add("Hangman", game);
            return View(game);
        }

        // get a random word from the database
        protected string GetRandomWord()
        {
            var Words = db.Words.ToList();
            if (Words.Count() == 0)
            {
                return "";
            }

            Random rand = new Random();
            int rnd = rand.Next(0, Words.Count() - 1);
            return Words[rnd].Name;
        }

        // Button-click handler
        [HttpPost]
        public ActionResult LetterChosen(string value)
        {
            // get data from session
            Game game = (Game)Session["Hangman"];
            // error input ---- shouldn't be possible
            if (value == null || value.Length == 0 || game.state != Game.eGameState.ACTIVE) {
                return View("Index", game);
            }

            // procedure works with upper case letters
            value = value.ToUpper();

            if (value.Length > 1)
            {
                // delete all but first character
                // note: should normaly only be a one character String
                value = value.Remove(1);
            }

            if (!Regex.IsMatch(value, "^[A-ZÖÄÜ]$")) {
                return View("Index", game);
            }
            // work with a char only from now on
            char letter = value[0];

            // game procedure
            // e.g. checking if letter is in word -> replacing "_" with letter in hiddenword
            game.LetterChosen(letter);

            // write game back into session
            Session["Hangman"] = game;

            return View("Index", game);
        }
    }
    
}