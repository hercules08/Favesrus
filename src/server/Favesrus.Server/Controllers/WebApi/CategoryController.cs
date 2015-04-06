﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Favesrus.Model.Entity;
using Favesrus.DAL.Impl;
using Favesrus.Services.Interfaces;

namespace Favesrus.Server.Controllers.WebApi
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService = null;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/Category
        public IQueryable<Category> GetCategories()
        {
            return _categoryService.AllCategories;
        }

        // GET api/Category/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = _categoryService.FindCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT api/Category/5
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                _categoryService.UpdateCategory(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Category
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _categoryService.UpdateCategory(category);

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE api/Category/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = _categoryService.FindCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryService.DeleteCategory(category.Id);

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            Category entity = _categoryService.FindCategoryById(id);
            return entity != null;
        }
    }
}