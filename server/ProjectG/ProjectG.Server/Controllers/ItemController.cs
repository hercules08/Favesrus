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
using ProjectG.Server.Models;
using ProjectG.Server.Interfaces;

namespace ProjectG.Server.Controllers
{
    public class ItemController : ApiController
    {
        private static List<Item> dontQueryList = new List<Item>();

        IRepository repo;

        static int countUp = 1;
        static int countDown; 

        public ItemController(IRepository repo)
        {
            this.repo = repo;
            countDown = repo.GetItems().Count();
        }

        // GET api/Item
        public IEnumerable<Item> GetItems()
        {
            return repo.GetItems();
        }

        [Route("api/item/getrandomitems")]
        [HttpGet]
        public IEnumerable<DumbItem> GetRandomItems()
        {
            List<Item> items = new List<Item>();

            //Get one random item not on the list
            Item itemA = repo.GetItem(countUp);
            while(itemA == null)
            {
                countUp++;
                itemA = repo.GetItem(countUp);
            }
            countUp++;
            Item itemB = repo.GetItem(countDown);
            countDown--;

            items.Add(itemA);
            items.Add(itemB);

            DumbItem dumbA = new DumbItem();
            dumbA.Id = itemA.Id;
            dumbA.ImageLink = itemA.ItemImage;
            DumbItem dumbB = new DumbItem();
            dumbB.Id = itemB.Id;
            dumbB.ImageLink = itemB.ItemImage;

            List<DumbItem> dumbList = new List<DumbItem>();
            dumbList.Add(dumbA);
            dumbList.Add(dumbB);

            return dumbList;
        }

    }
}