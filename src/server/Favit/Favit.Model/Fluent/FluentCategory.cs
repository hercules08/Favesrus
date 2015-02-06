using Favit.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Model.Fluent
{
    public interface IFluentCategory
    {
        IFluentCategory CategoryName(string categoryName);
        Category Create();
        Category Create(string categoryName);
        Category Create(string categoryName, ICollection<Item> items);
    }

    public class FluentCategory : IFluentCategory
    {
        private static Category category;

        private static IFluentCategory fluent;


        static FluentCategory()
        {
            fluent = new FluentCategory();
        }

        public static IFluentCategory Init()
        {
            category = new Category();
            return fluent;
        }

        public IFluentCategory CategoryName(string categoryName)
        {
            category.CategoryName = categoryName;
            return fluent;
        }

        public Category Create()
        {
            return category;
        }

        public Category Create(string categoryName)
        {
            CategoryName(categoryName);
            return category;
        }

        public Category Create(string categoryName, ICollection<Item> items)
        {
            CategoryName(categoryName);
            AddItemsToCategory(items);
            return category;
        }

        private static void AddItemsToCategory(ICollection<Item> items)
        {
            foreach(var item in items)
            {
                category.AddItemToCategory(item);
            }
        }
    }
}
