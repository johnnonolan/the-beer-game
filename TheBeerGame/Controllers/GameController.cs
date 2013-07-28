using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TheBeerGame.Models;

namespace TheBeerGame.Controllers
{
    public class GameController : Controller
    {
        //
        // POST: /Game/Create



        //[HttpPost]
        public ActionResult Create()
        {
            var model = new GameStatusViewModel();
            return View(model);     
        }



    }
}
