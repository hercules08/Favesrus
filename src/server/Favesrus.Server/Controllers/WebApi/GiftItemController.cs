using AutoMapper;
using Favesrus.DAL.Impl;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto.GiftItem;
using Favesrus.Server.Filters;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models.Recommendation;
using Favesrus.Server.Processing;
using Favesrus.Server.Processing.Interface;
using System.Collections.Generic;
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
    //[Authorize]
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
        [HttpGet]
        [Route("getgiftitems")]
        public IHttpActionResult GetGiftItems()
        {
            string successStatus = "get_all_giftItems";
            string successMessage = "Successfully retireved gift items.";

            var giftItems = db.GiftItems.ToList();

            return new BaseActionResult<ICollection<GiftItem>>(
                Request,
                giftItems,
                successMessage,
                successStatus);
        }

        // Gets the gifitem sets from the user provided
        // recommendations list
        [ValidateModel]
        [HttpPost]
        [Route("gettotlist")]
        public async Task<IHttpActionResult> GetToTList(
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
        [Route("getgiftitemsbycategoryid")]
        public IHttpActionResult GetGiftItemsByCategoryId(HttpRequestMessage requestMessage, int categoryId)
        {
            var giftItems = db.GiftItems.Where(g => g.Category.Where(c => c.Id == categoryId).Count() != 0);

            List<DtoGiftItem> dtoGiftItems = new List<DtoGiftItem>();

            foreach(var giftItem in giftItems)
            {
                dtoGiftItems.Add(Mapper.Map<DtoGiftItem>(giftItem));
            }

            return new BaseActionResult<ICollection<DtoGiftItem>>(requestMessage, dtoGiftItems, "Found gift items for the category", "found_giftitems_for_categoryId");
        }

        [HttpGet]
        [Route("getgiftitemswithterm")]
        public IHttpActionResult GetGiftItemsWithTerm(HttpRequestMessage requestMessage, string searchText)
        {
            var term = searchText.ToLower();

            var searchResults = db.GiftItems.Where(g => g.ItemName.ToLower().Contains(term)
                || g.Description.ToLower().Contains(term)).ToList();

            List<DtoGiftItem> dtoGiftItems = new List<DtoGiftItem>();
            //Add dummy 
            dtoGiftItems.Add(new DtoGiftItem() { Image = "dummy.jpg", Name = "dummy", Description = "dummy description" });
            dtoGiftItems.Add(new DtoGiftItem() { Image = "dummy2.jpg", Name = "dummy2", Description = "dummy2 description" });
            foreach (var giftItem in searchResults)
            {
                dtoGiftItems.Add(Mapper.Map<DtoGiftItem>(giftItem));
            }


            return new BaseActionResult<ICollection<DtoGiftItem>>(requestMessage, dtoGiftItems, "Found Matches", "matching_products");
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