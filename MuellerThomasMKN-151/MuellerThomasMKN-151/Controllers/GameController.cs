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

        // GET: Game
        public ActionResult Index() {
            Game game = new Game(1);
            game.completeWord   = GetRandomWord();
            game.hiddenWord     = Game.CreateHiddenWord (game.completeWord);
            ViewData ["Word"]   = Game.InsertSpaces(game.hiddenWord);

            Session.Add("Hangman", game);
            return View(game);
        }

        
        protected string GetRandomWord()
        {
            var Words = db.Words.ToList();

            Random rand = new Random();
            int rnd = rand.Next(0, Words.Count() - 1);
            return Words[rnd].Name;
        }


        [HttpPost]
        public ActionResult LetterChosen(string value)
        {
            Game game = (Game)Session["Hangman"];
            // error input ---- shouldn't be possible
            if (value == null || value.Length == 0) {
                return View();
            }

            char letter = value[0];

            game.LetterChosen(letter);

            Session["Hangman"] = game;
            ViewData["Word"] = Game.InsertSpaces(game.hiddenWord);
            return View("Index", game);
        }




    }
    
}