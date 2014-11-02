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
    public class ItemGenerateController : Controller
    {
        IRepository repo;

        public ItemGenerateController(IRepository repo)
        {
            this.repo = repo;
        }


        // GET: /ItemGenerate/
        public ActionResult Index()
        {
            var items = repo.GetItems();
            return View(items.ToList());
        }

        // GET: /ItemGenerate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = repo.GetItem(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: /ItemGenerate/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(repo.GetCategories(), "Id", "CategoryName");
            ViewBag.RetailerId = new SelectList(repo.GetRetailers(), "Id", "RetailerName");
            return View();
        }

        // POST: /ItemGenerate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ItemName,ItemPrice,RetailerId,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                repo.AddItem(item);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(repo.GetCategories(), "Id", "CategoryName", item.CategoryId);
            ViewBag.RetailerId = new SelectList(repo.GetRetailers(), "Id", "RetailerName", item.RetailerId);
            return View(item);
        }

        // GET: /ItemGenerate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = repo.GetItem(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(repo.GetCategories(), "Id", "CategoryName", item.CategoryId);
            ViewBag.RetailerId = new SelectList(repo.GetRetailers(), "Id", "RetailerName", item.RetailerId);
            return View(item);
        }

        // POST: /ItemGenerate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ItemName,ItemPrice,RetailerId,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateItem(item);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(repo.GetCategories(), "Id", "CategoryName", item.CategoryId);
            ViewBag.RetailerId = new SelectList(repo.GetRetailers(), "Id", "RetailerName", item.RetailerId);
            return View(item);
        }

        // GET: /ItemGenerate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = repo.GetItem(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /ItemGenerate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}
