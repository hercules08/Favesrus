﻿using Favit.Server.Interfaces;
using Favit.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Favit.Server.BLL.Repos
{
    public class Repository : IRepository
    {
        private ProjectGDBContext context = new ProjectGDBContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public User GetUser(int id)
        {
            return context.Users.Find(id);
        }

        public void SaveUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Retailer AddRetailer(Retailer retailer)
        {
            Retailer localRetailer = context.Retailers.Add(retailer);
            context.SaveChanges();
            return localRetailer;
        }

        public IEnumerable<Retailer> GetRetailers()
        {
            return context.Retailers;
        }

        public Retailer GetRetailer(int? id)
        {
            return context.Retailers.Find(id.Value);
        }

        public Retailer UpdateRetailer(Retailer retailer)
        {
            context.Entry(retailer).State = EntityState.Modified;
            context.SaveChanges();
            return retailer;
        }


        public IEnumerable<Category> GetCategories()
        {
            return context.Categories;
        }

        public Category GetCategory(int? id)
        {
            return context.Categories.Find(id.Value);
        }

        public Category AddCategory(Category category)
        {
            Category localCategory = context.Categories.Add(category);
            context.SaveChanges();
            return localCategory;
        }

        public Category UpdateCategory(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
            return category;
        }


        public IEnumerable<Item> GetItems()
        {
            return context.Items.Include(i => i.Category).Include(i => i.Retailer);
        }

        public Item GetItem(int? id)
        {
            return context.Items.Find(id.Value);
        }

        public Item AddItem(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            return item;
        }

        public Item UpdateItem(Item item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            return item;
        }

        public void DeleteItem(int id)
        {
            Item item = context.Items.Find(id);
            context.Items.Remove(item);
            context.SaveChanges();
        }


        public int GetXBoxCount()
        {
            return context.VoteModels.First().XBoxCount;
        }

        public int GetPS4Count()
        {
            return context.VoteModels.First().PS4Count;
        }

        public void IncrementXBoxCount()
        {
            VoteModel model = context.VoteModels.Find(1) ;
            model.XBoxCount += 1;
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void IncrementPS4Count()
        {
            VoteModel model = context.VoteModels.Find(1);
            model.PS4Count += 1;
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public VoteModel GetVoteModel()
        {
            return context.VoteModels.First();
        }
    }
}