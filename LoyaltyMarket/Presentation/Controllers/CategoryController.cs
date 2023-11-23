using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[RequireHttps]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categorysService;

        public CategoryController(ICategoryService categorysService) =>
            _categorysService = categorysService;

        
        /// <summary>
        /// Create the category document
        /// </summary>
        /// <returns> </returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryRequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(CategoryRequestModel newCategory)
        {
            
            try
            {
                if (string.IsNullOrEmpty(newCategory.Name) || string.IsNullOrEmpty(newCategory.Description))
                {
                    return BadRequest();
                }

                await _categorysService.CreateAsync(newCategory);

                return Ok();
            }
            catch(Exception exception)
            {
                return NotFound(exception);
            }
        }

        /// <summary>
        /// Return all the categories for list porpouse
        /// </summary>
        /// <returns>List of Categories Summarized</returns>
        
        [ProducesResponseType(typeof(IEnumerable<CategoryRequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            try
            {
                var modelList = await _categorysService.GetAllAsync(token);

                return Ok(modelList);
            }
            catch(Exception exception)
            {
                return NotFound(exception);
            }
        }
        
        /// <summary>
        /// Returns an specific category selected by the Id
        /// </summary>
        /// <returns> </returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryRequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var category = await _categorysService.GetById(id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch(Exception exception)
            {
                return NotFound(exception);
            }

        }
        
        /// <summary>
        /// Updates the category fields based on the provided Id
        /// </summary>
        /// <returns> </returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryRequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]CategoryUpdateModel categoryToUpdate)
        {
            try
            {                
                await _categorysService.UpdateAsync(id, categoryToUpdate);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete the category document based on the provided Id
        /// </summary>
        /// <returns> </returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryRequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _categorysService.RemoveAsync(id);
                
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}