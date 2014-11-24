using Favit.Model.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;

namespace Favit.Model.Entities
{
    public interface ICategory
    {
        [NotNullValidator(ErrorMessage="The category cannot be null or empty")]
        string CategoryName { get; set; }
        ICollection<Item> Items { get; set; }
    }

    public partial class Category:EntityBase, ICategory
    {
        internal Category()
        {
            this.Items = new List<Item>();
        }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        public Category(string categoryName, ICollection<Item> items):
            this(categoryName)
        {
            Items = items;
        }

        public virtual string CategoryName { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public void AddItemToCategory(Item item)
        {
            Items.Add(item);
        }
    }
}
