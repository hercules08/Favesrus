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

        public ItemController(IRepository repo)
        {
            this.repo = repo;
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
            Item itemA = repo.GetItems().Except(dontQueryList).First();
            dontQueryList.Add(itemA);
            Item itemB = repo.GetItems().Except(dontQueryList).Last();
            dontQueryList.Add(itemB);

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