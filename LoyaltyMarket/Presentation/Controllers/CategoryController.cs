using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using MongoDB.Bson.IO;

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
        /// Return all the categories for list porpouse
        /// </summary>
        /// <returns>List of Categories Summarized</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategorySummaryResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
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

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]CategoryRequestModel categoryToUpdate)
        {
           try
            { 
                Console.WriteLine("chegou no controller");
                await _categorysService.UpdateAsync(id, categoryToUpdate);
                return Ok();
            }
            catch(Exception exception)
            {
                return BadRequest(exception);
            }
        }

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