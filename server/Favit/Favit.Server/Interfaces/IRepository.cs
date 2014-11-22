using Favit.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favit.Server.Interfaces
{
    public interface IRepository
    {
        IEnumerable<User> Users { get; }
        User GetUser(int id);
        void SaveUser(User user);

        IEnumerable<Retailer> GetRetailers();
        Retailer GetRetailer(int? id);
        Retailer AddRetailer(Retailer retailer);
        Retailer UpdateRetailer(Retailer retailer);
        IEnumerable<Category> GetCategories();
        Category GetCategory(int? id);
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);

        IEnumerable<Item> GetItems();
        Item GetItem(int? id);
        Item AddItem(Item item);
        Item UpdateItem(Item item);
        void DeleteItem(int id);

        int GetXBoxCount();
        int GetPS4Count();

        void IncrementXBoxCount();
        void IncrementPS4Count();

        VoteModel GetVoteModel();
    }
}