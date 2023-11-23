using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Domain.Models.CategoryModels;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categorysService;

        public CategoryController(ICategoryService categorysService) =>
            _categorysService = categorysService;

        
        /// <summary>
        /// Create the category document
        /// </summary>
        /// <returns> Returns ok when the cateogry is created successfully</returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(CategoryRequestModel newCategory, CancellationToken cancellationToken)
        {
            
            try
            {
                if (string.IsNullOrEmpty(newCategory.Name) || string.IsNullOrEmpty(newCategory.Description))
                {
                    return BadRequest();
                }

                await _categorysService.CreateAsync(newCategory, cancellationToken);

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
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var modelList = await _categorysService.GetAllAsync(cancellationToken);

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
        /// <returns> Return the provided category selected by Id </returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categorysService.GetById(id, cancellationToken);

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
        /// <returns> Updates the provided category based on the Id</returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]CategoryUpdateModel categoryToUpdate, CancellationToken cancellationToken)
        {
            try
            {                
                await _categorysService.UpdateAsync(id, categoryToUpdate, cancellationToken);
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
        /// <returns> Return ok when the category is delte successfully</returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                await _categorysService.RemoveAsync(id, cancellationToken);
                
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}