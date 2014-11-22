using Favit.Server.Interfaces;
using Favit.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Favit.Server.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;

        public HomeController(IRepository repo)
        {
            this.repo = repo;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult VoteStatus()
        {
            VoteModel voteModel = repo.GetVoteModel();
            return View(voteModel);
        }
    }
}
