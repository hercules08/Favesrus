using Favesrus.DAL.Abstract;
using Favesrus.Model.Entity;
using Favesrus.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _uow = null;
        private readonly IRepository<Category> _categoryRepo = null;

        public CategoryService(
            IUnitOfWork uow,
            IRepository<Category> categoryRepo)
        {
            _uow = uow;
            categoryRepo = _categoryRepo;
        }

        public IQueryable<Category> AllCategories
        {
            get { return _categoryRepo.All; }
        }
    }
}
