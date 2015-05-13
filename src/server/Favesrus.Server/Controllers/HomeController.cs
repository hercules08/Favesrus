﻿using System.Web.Mvc;

namespace Favesrus.Server.Controllers
{
    [Authorize(Roles="Admin")]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
	}
}