using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.Results.Error;
using Favesrus.Core.TypeMapping;
using Favesrus.DAL.Core;
using Favesrus.Data.Dtos;
using Favesrus.Domain.Entity;
using System.Linq;

namespace Favesrus.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> AllCategories { get; }
        Category AddCategory(Category entity);
        Category UpdateCategory(Category entity);
        CategoryModel FindCategoryById(int id);
        Category FindCategoryByName(string name);
        void DeleteCategory(int id);
    }
    public class CategoryService:BaseService,ICategoryService
    {
        private readonly IUnitOfWork _uow = null;
        private readonly IRepository<Category> _categoryRepo = null;

        public CategoryService(
            ILogManager logManager,
            IAutoMapper mapper,
            IUnitOfWork uow,
            IRepository<Category> categoryRepo)
            :base(logManager, mapper)
        {
            _uow = uow;
            _categoryRepo = categoryRepo;
        }

        public IQueryable<Category> AllCategories
        {
            get { return _categoryRepo.All.OfType<Category>(); }
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

        public CategoryModel FindCategoryById(int id)
        {

            var category = _categoryRepo.FindById(id);
            
            if (category == null)
            {
                string errorMessage = string.Format("Category with id {0} not found",id);
                Logger.Error(errorMessage);
                throw new ApiErrorException(errorMessage);
            }

            CategoryModel categoryModel = Mapper.Map<CategoryModel>(category);

            return categoryModel;
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
