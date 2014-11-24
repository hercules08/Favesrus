

using Favit.BLL.Services;
using Favit.DAL.EntityFramwork;
using Favit.DAL.Interfaces;
using Favit.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Favit.Server.Controllers
{
    public class HomeController : Controller
    {
        ISessionFactory sessionFactory;
        IUnitOfWork uow;
        IRepository repo;

        public HomeController(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            uow = sessionFactory.CurrentUoW;
            repo = new Repository(uow);
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public string Subscription(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                newsletter emailExists = repo.GetList<newsletter>(n => n.email.ToLower() == email.ToLower()).FirstOrDefault();

                if (emailExists == null && IsValidEmail(email))
                {
                    // Add to database
                    uow.BeginTransaction();
                    repo.AddEntity(new newsletter(email));
                    uow.CommitTransaction();

                    return "success";
                }
                else
                {
                    return "subscribed";
                }
            }
            else
            {
                return "invalid";
            }
        }

        [HttpPost]
        public string WriteUs(string email, string name, string message)
        {
            //check if email is valid
            if (IsValidEmail(email))
            {
                string to = ConfigurationManager.AppSettings["AdminEmailAddresses"];
                //Send the message
                new EmailService().SendEmail(email, name + " - Favit Message", message, to);
                return "success";
            }
            else
            {
                return "invalid";
            }
            //
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        //public ActionResult VoteStatus()
        //{
        //    VoteModel voteModel = repo.GetVoteModel();
        //    return View(voteModel);
        //}
    }
}
