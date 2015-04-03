using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Favesrus.Model.Entity;
using Favesrus.DAL.Impl;

namespace Favesrus.Server.Controllers
{
    public class GiftItemController : Controller
    {
        private FavesrusDbContext db = new FavesrusDbContext();

        // GET: /GiftItem/
        public ActionResult Index()
        {
            return View(db.GiftItems.ToList());
        }

        // GET: /GiftItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiftItem giftitem = db.GiftItems.Find(id);
            if (giftitem == null)
            {
                return HttpNotFound();
            }
            return View(giftitem);
        }

        // GET: /GiftItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /GiftItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ItemName,ItemImage,ItemPrice")] GiftItem giftitem)
        {
            if (ModelState.IsValid)
            {
                db.GiftItems.Add(giftitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giftitem);
        }

        // GET: /GiftItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiftItem giftitem = db.GiftItems.Find(id);
            if (giftitem == null)
            {
                return HttpNotFound();
            }
            return View(giftitem);
        }

        // POST: /GiftItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ItemName,ItemImage,ItemPrice")] GiftItem giftitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giftitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giftitem);
        }

        // GET: /GiftItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiftItem giftitem = db.GiftItems.Find(id);
            if (giftitem == null)
            {
                return HttpNotFound();
            }
            return View(giftitem);
        }

        // POST: /GiftItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiftItem giftitem = db.GiftItems.Find(id);
            db.GiftItems.Remove(giftitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
