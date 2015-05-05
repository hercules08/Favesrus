using Favesrus.DAL.Impl;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Exceptions;
using Favesrus.Server.Filters;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models.Recommendation;
using Favesrus.Server.Processing;
using Favesrus.Server.Processing.ActionResult;
using Favesrus.Server.Processing.Interface;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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


        IAutoMapper _mapper;
        IRecommendationsProcessor _recommendationsProcessor;

        public GiftItemController(IAutoMapper mapper,
            IRecommendationsProcessor recommendationProcessor)
        {
            _mapper = mapper;
            _recommendationsProcessor = recommendationProcessor;
        }

        public GiftItemController()
        {

        }

        // GET api/GiftItem
        public IQueryable<GiftItem> GetGiftItems()
        {
            return db.GiftItems;
        }

        // Gets the gifitem sets from the user provided
        // recommendations list
        [ValidateModel]
        public async Task<IHttpActionResult> GetToT(
            HttpRequestMessage requestMessage,
            GetRecommendationsModel model)
        {
            string successStatus = "get_recommendations_success";
            string successMessage = "Successfully retireved recommendations";

            ICollection<DtoGiftItem> giftItems = await _recommendationsProcessor.GetToTAsync(model);

            return new BaseActionResult<ICollection<DtoGiftItem>>(
                requestMessage,
                giftItems,
                successMessage,
                successStatus);
        }

        [HttpGet]
        [Route("getgiftitemsbycatgoryid")]
        public IHttpActionResult GetGiftItemsByCategoryId(HttpRequestMessage requestMessage, int categoryId)
        {
            var results = db.GiftItems.Where(g => g.Category.Where(c => c.Id == categoryId).Count() != 0);
            return new BaseActionResult<IEnumerable<GiftItem>>(requestMessage, results, "Found gift items for the category", "found_giftitems_for_categoryId");
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