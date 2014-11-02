using ProjectG.Server.Interfaces;
using ProjectG.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectG.Server.BLL.Repos
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

        public void SaveUser(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
}