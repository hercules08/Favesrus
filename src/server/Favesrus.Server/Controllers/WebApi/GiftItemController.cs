using Favesrus.DAL.Impl;
using Favesrus.Model.Entity;
using Favesrus.Server.Exceptions;
using Favesrus.Server.Filters;
using Favesrus.Server.Processing;
using Favesrus.Server.Processing.ProcessingFavesrusUser.ActionResult;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;


namespace Favesrus.Server.Controllers.WebApi
{
    [Authorize]
    [RoutePrefix("api/giftItem")]
    public class GiftItemController : ApiBaseController
    {
        private FavesrusDbContext db = new FavesrusDbContext();

        // GET api/GiftItem
        public IQueryable<GiftItem> GetGiftItems()
        {
            return db.GiftItems;
        }

        public class WishListAddModel
        {
            [Required]
            public string UserId { get; set; }
            [Required]
            public int GiftItemId { get; set; }
            [Required]
            public int WishListId { get; set; }
        }

        [HttpGet]
        [Route("getgiftitemsbycatgoryid")]
        public IHttpActionResult GetGiftItemsByCategoryId(HttpRequestMessage requestMessage, int categoryId)
        {
            var results = db.GiftItems.Where(g => g.Category.Where(c => c.Id == categoryId).Count() != 0);
            return new BaseActionResult<IEnumerable<GiftItem>>(requestMessage, results, "Found gift items for the category", "found_giftitems_for_categoryId");
        }


        [HttpPost]
        [Route("addgiftitemtowishlist")]
        //[Authorize]
        [ValidateModel]
        public IHttpActionResult AddGiftItemToWishlist(HttpRequestMessage requestMessage, WishListAddModel model)
        {
            var user = UserManager.FindById(model.UserId);

            if(user != null)
            {
                var foundItem = db.GiftItems.Find(model.GiftItemId);

                if(foundItem != null)
                {
                    var foundWishList = db.WishLists.Find(model.WishListId);

                    //if(user.WishListItems.Where(g => g.Id == foundItem.Id).Count() == 0)
                    //{
                    //    user.WishListItems.Add(foundItem);
                    //    db.Users.Attach(user);
                    //    db.SaveChanges();

                    //    return new BaseActionResult<string>(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                    //}

                    return new BaseActionResult<string>(requestMessage, "Successful add to wishlist", "Successful add to wishlist", "successful_wishlist_add");
                }
                throw new BusinessRuleException("giftitem_not_found", "The gift item could not be found");
            }
            throw new BusinessRuleException("user_not_found", "The user could not be found.");
        }

        [HttpGet]
        public IHttpActionResult GetGiftItemsWithTerm(HttpRequestMessage requestMessage, string searchText)
        {
            var term = searchText.ToLower();

            var searchResults = db.GiftItems.Where(g => g.ItemName.ToLower().Contains(term)
                || g.Description.ToLower().Contains(term)).ToList();

            return new BaseActionResult<IEnumerable<GiftItem>>(requestMessage, searchResults, "Found Matches", "matching_products");
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