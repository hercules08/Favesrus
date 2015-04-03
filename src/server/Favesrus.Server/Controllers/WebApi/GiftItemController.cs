using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Favesrus.Model.Entity;
using Favesrus.DAL.Impl;

namespace Favesrus.Server.Controllers.WebApi
{
    public class GiftItemController : ApiController
    {
        private FavesrusDbContext db = new FavesrusDbContext();

        // GET api/GiftItem
        public IQueryable<GiftItem> GetGiftItems()
        {
            return db.GiftItems;
        }

        // GET api/GiftItem/5
        [ResponseType(typeof(GiftItem))]
        public IHttpActionResult GetGiftItem(int id)
        {
            GiftItem giftitem = db.GiftItems.Find(id);
            if (giftitem == null)
            {
                return NotFound();
            }

            return Ok(giftitem);
        }

        // PUT api/GiftItem/5
        public IHttpActionResult PutGiftItem(int id, GiftItem giftitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != giftitem.Id)
            {
                return BadRequest();
            }

            db.Entry(giftitem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiftItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/GiftItem
        [ResponseType(typeof(GiftItem))]
        public IHttpActionResult PostGiftItem(GiftItem giftitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GiftItems.Add(giftitem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = giftitem.Id }, giftitem);
        }

        // DELETE api/GiftItem/5
        [ResponseType(typeof(GiftItem))]
        public IHttpActionResult DeleteGiftItem(int id)
        {
            GiftItem giftitem = db.GiftItems.Find(id);
            if (giftitem == null)
            {
                return NotFound();
            }

            db.GiftItems.Remove(giftitem);
            db.SaveChanges();

            return Ok(giftitem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GiftItemExists(int id)
        {
            return db.GiftItems.Count(e => e.Id == id) > 0;
        }
    }
}