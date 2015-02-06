using Favit.BLL.Interfaces;
using Favit.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;



namespace Favit.Server.Controllers
{
    public class RetailerController : Controller
    {

        IRetailerService retailerService;

        public RetailerController(IRetailerService retailerService)
        {
            this.retailerService = retailerService;
        }


        // GET: /Retailer/
        public ActionResult Index()
        {
            return View(retailerService.GetRetailers());
        }

        // GET: /Retailer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = retailerService.FindRetailerById(id);
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
        public ActionResult Create([Bind(Include = "Id,RetailerName,RetailerLogo,RetailerLogoDataString")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                retailerService.AddRetailer(retailer);
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
            Retailer retailer = retailerService.FindRetailerById(id);
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
        public ActionResult Edit([Bind(Include = "Id,RetailerName,RetailerLogo,RetailerLogoDataString")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                retailerService.UpdateRetailer(retailer);
                return RedirectToAction("Index");
            }
            return View(retailer);
        }

    }
}
