using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categorysService;

        public CategoryController(ICategoryService categorysService) =>
            _categorysService = categorysService;

        // TODO : ensure category id is empty to be assigned by the database
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
            {await _categorysService.CreateAsync(newCategory);

            return Ok();
            }catch(Exception exception){
                return BadRequest(exception);
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
            catch
            {
                return BadRequest();
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

                return Ok(category);
            }
            catch
            {
                return BadRequest();
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
            catch(Exception exception)
            {
                return BadRequest(exception);
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
                return BadRequest();
            }
        }
    }
}