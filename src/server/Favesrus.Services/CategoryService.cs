using Favesrus.Core;
using Favesrus.DAL.Core;
using Favesrus.Domain.Entity;
using System.Linq;

namespace Favesrus.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> AllCategories { get; }
        Category AddCategory(Category entity);
        Category UpdateCategory(Category entity);
        Category FindCategoryById(int id);
        Category FindCategoryByName(string name);
        void DeleteCategory(int id);
    }
    public class CategoryService:BaseService,ICategoryService
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


        public Category AddCategory(Category entity)
        {
            _categoryRepo.Add(entity);
            _uow.Commit();
            return entity;
        }

        public Category UpdateCategory(Category entity)
        {
            _categoryRepo.Update(entity);
            _uow.Commit();
            return entity;
        }

        public Category FindCategoryById(int id)
        {
            return _categoryRepo.FindById(id);
        }

        public Category FindCategoryByName(string name)
        {
            return _categoryRepo.FindWhere(c => c.CategoryName == name);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepo.DeleteWhere(c => c.Id == id);
            _uow.Commit();
        }
    }
}
