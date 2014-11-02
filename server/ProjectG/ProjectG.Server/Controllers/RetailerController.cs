using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectG.Server.Models;
using ProjectG.Server.Interfaces;

namespace ProjectG.Server.Controllers
{
    public class RetailerController : Controller
    {
        IRepository repo;


        public RetailerController(IRepository repo)
        {
            this.repo = repo;
        }


        // GET: /Retailer/
        public ActionResult Index()
        {
            return View(repo.GetRetailers());
        }

        // GET: /Retailer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = repo.GetRetailer(id);
            if (retailer == null)
            {
                return HttpNotFound();
            }
            return View(retailer);
        }

        // GET: /Retailer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Retailer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,RetailerName,RetailerLogo,RetailerLogoDataString")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                repo.AddRetailer(retailer);
                return RedirectToAction("Index");
            }

            return View(retailer);
        }

        // GET: /Retailer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = repo.GetRetailer(id);
            if (retailer == null)
            {
                return HttpNotFound();
            }
            return View(retailer);
        }

        // POST: /Retailer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,RetailerName,RetailerLogo,RetailerLogoDataString")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateRetailer(retailer);
                return RedirectToAction("Index");
            }
            return View(retailer);
        }

    }
}
