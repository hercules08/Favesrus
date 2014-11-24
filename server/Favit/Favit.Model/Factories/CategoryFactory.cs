using Favit.Model.Base;
using Favit.Model.Entities;
using Favit.Model.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Model.Factories
{
    public class CategoryFactory
    {
        static Category entity;

        public static Category Create()
        {
            return FluentCategory.Init().Create();            
        }

        public static Category Create(string categoryName)
        {
            entity = FluentCategory.Init().Create(categoryName);

            if(entity.IsValid)
            {
                return entity;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public static Category Create(string categoryName, ICollection<Item> items)
        {
            entity = FluentCategory.Init().Create(categoryName, items);
            
            if (entity.IsValid)
            {
                return entity;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
