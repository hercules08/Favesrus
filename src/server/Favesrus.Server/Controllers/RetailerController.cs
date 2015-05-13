using Favesrus.Model.Entity;
using Favesrus.Services.Interfaces;
using System.Net;
using System.Web.Mvc;

namespace Favesrus.Server.Controllers
{
    public class RetailerController : Controller
    {

        private readonly IRetailerService _retailerService = null;

        public RetailerController(IRetailerService retailerService)
        {
            _retailerService = retailerService;
        }

        // GET: /Retailer/
        public ActionResult Index()
        {
            return View(_retailerService.GetRetailers());
        }


        // GET: /Retailer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = _retailerService.FindRetailerById(id.Value);
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
        public ActionResult Create([Bind(Include = "Id,RetailerName,RetailerLogo,RetailerLogoDataString,ModoMerchantId")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                _retailerService.AddRetailer(retailer);
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
            Retailer retailer = _retailerService.FindRetailerById(id.Value);
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
        public ActionResult Edit([Bind(Include = "Id,RetailerName,RetailerLogo,RetailerLogoDataString,ModoMerchantId")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                _retailerService.UpdateRetailer(retailer);
                return RedirectToAction("Index");
            }
            return View(retailer);
        }

        // GET: /Retailer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = _retailerService.FindRetailerById(id.Value);
            if (retailer == null)
            {
                return HttpNotFound();
            }
            return View(retailer);
        }

        // POST: /Retailer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Retailer retailer = _retailerService.FindRetailerById(id);
            _retailerService.DeleteRetailer(retailer.Id);
            return RedirectToAction("Index");
        }
    }
}
