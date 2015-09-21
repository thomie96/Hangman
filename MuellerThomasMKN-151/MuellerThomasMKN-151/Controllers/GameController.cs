using MuellerThomasMKN_151.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuellerThomasMKN_151.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        private Game gameData = new Game (1);
        // GET: Game
        public ActionResult Index()
        {
            gameData.completeWord = GetRandomWord();
            gameData.showWord = CreateShowWord (gameData.completeWord);
            ViewData ["Word"] = gameData.showWord;
            return View();
        }

        
        protected string GetRandomWord()
        {
            var Words = db.Words.ToList();

            Random rand = new Random();
            int rnd = rand.Next (0, Words.Count()-1);

            return Words[rnd].Name;
        }


        public string CreateShowWord (string completeWord)
        {
            string showWord = "";
            for (int i = 0; i < completeWord.Length; i++) 
            {
                showWord += "_ ";
            }
            return showWord.TrimEnd (' ');
        }


        [HttpPost]
        public ActionResult LetterChosen(string letter)
        {
            if (letter.Equals("A")) {
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
    
}