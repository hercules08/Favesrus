using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using Favesrus.Data.Dtos;
using Favesrus.Domain.Entity;
using Favesrus.Results;
using Favesrus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public interface ICategoryController
    {
        IHttpActionResult GetCategories();
        IHttpActionResult GetCategory(int id);
        //IHttpActionResult PutCategory(int id, CategoryModel categoryModel);
        //IHttpActionResult PostCategory(CategoryModel category);
        //IHttpActionResult DeleteCategory(int id);
    }

    public class CategoryController : BaseApiController, ICategoryController
    {
        private readonly ICategoryService _categoryService = null;

        public CategoryController
            (ILogManager logManager,
            IAutoMapper mapper,
            ICategoryService categoryService)
            :base(logManager, mapper)
        {
            _categoryService = categoryService;
        }

        // GET api/Category
        public IHttpActionResult GetCategories()
        {
            Logger.Info("Begin");

            string apiStatus = "get_all_categories";
            string apiMessage = "Retrieved all Faves Categories";

            var categories = _categoryService.AllCategories.ToList();

            ICollection<CategoryModel> allCategories = Mapper.Map<ICollection<CategoryModel>>(categories);

            Logger.Info("End");
            return new ApiActionResult<ICollection<CategoryModel>>
                (apiStatus, apiMessage, allCategories);
        }

        public IHttpActionResult GetCategory(int id)
        {
            Logger.Info("Begin");

            string apiStatus = "get_category";
            string apiMessage = "Successfully retrieved category";

            CategoryModel categoryModel = _categoryService.FindCategoryById(id);

            Logger.Info("End");
            
            return new ApiActionResult<CategoryModel>
            (apiStatus, apiMessage, categoryModel);
        }

        //// PUT api/Category/5
        //public IHttpActionResult PutCategory(int id, Category category)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != category.Id)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        _categoryService.UpdateCategory(category);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST api/Category
        //[ResponseType(typeof(Category))]
        //public IHttpActionResult PostCategory(Category category)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _categoryService.UpdateCategory(category);

        //    return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        //}

        //// DELETE api/Category/5
        //[ResponseType(typeof(Category))]
        //public IHttpActionResult DeleteCategory(int id)
        //{
        //    Category category = _categoryService.FindCategoryById(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _categoryService.DeleteCategory(category.Id);

        //    return Ok(category);
        //}

        //private bool CategoryExists(int id)
        //{
        //    Category entity = _categoryService.FindCategoryById(id);
        //    return entity != null;
        //}
    }
}
