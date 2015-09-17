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
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        
        protected string getRandomWord()
        {
            var Words = db.Words.ToList();

            Random rand = new Random();
            int rnd = rand.Next (1, Words.Count());

            return Words[rnd].Name;
        }
    }
    
}