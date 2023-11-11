using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services;
using Presentation.Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categorysService;

        public CategoryController(CategoryService categorysService) =>
            _categorysService = categorysService;

        [HttpGet]
        public async Task<List<Category>> Get() =>
            await _categorysService.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var Category = await _categorysService.GetByIdAsync(id);

            if (Category is null)
            {
                return NotFound();
            }

            return Category;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Category newCategory)
        {
            await _categorysService.CreateAsync(newCategory);

            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Category updatedCategory)
        {
            var Category = await _categorysService.GetByIdAsync(id);

            if (Category is null)
            {
                return NotFound();
            }

            updatedCategory.Id = Category.Id;

            await _categorysService.UpdateAsync(id, updatedCategory);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Category = await _categorysService.GetByIdAsync(id);

            if (Category is null)
            {
                return NotFound();
            }

            await _categorysService.RemoveAsync(id);

            return NoContent();
        }
    }
}