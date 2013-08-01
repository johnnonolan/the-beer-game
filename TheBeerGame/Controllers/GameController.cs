using System.Web.Mvc;
//using Brewery;
using B = Brewery;

namespace TheBeerGame.Controllers
{
    public class GameController : Controller
    {

        
        public ActionResult Create()
        {
            var brewery = new B.Brewery();
            var model = brewery.CreateGame();
            return View("Index",model);     
        }


        public ActionResult EndTurn()
        {
            var brewery = new B.Brewery();
            var model = brewery.EndTurn();

            return View("Index", model);
        }
    }
}
