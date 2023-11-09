using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoyaltyMarket.Models;
using Microsoft.AspNetCore.Mvc;
using LoyaltyMarket.Server.Services;

namespace LoyaltyMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _categoryService.GetCategories();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            var createdCategory = await _categoryService.CreateCategory(category);
            return CreatedAtAction(nameof(Get), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Category updatedCategory)
        {
            var success = await _categoryService.UpdateCategory(id, updatedCategory);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _categoryService.DeleteCategory(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}