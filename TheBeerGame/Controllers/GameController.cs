using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Boozy;

namespace TheBeerGame.Controllers
{
    public class GameController : Controller
    {
        readonly IBrewery _brewery;

        public GameController(IBrewery brewery)
        {
            _brewery = brewery;
        }

        public ActionResult Create()
        {
            var brewery = new Brewery();
            var model = brewery.CreateGame();
            return View("Index", model);   
        }


        public ActionResult TakeTurn(Guid gameId, int retailOrder)
        {
            var model = _brewery.EndTurn(gameId,5,retailOrder);
            return View("Index", model);
        }
    }
}
