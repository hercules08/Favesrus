using Favesrus.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<Category> AllCategories { get; }
    }
}
