using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductController(IProductService ProductsService) =>
            _productsService = ProductsService;

        // TODO : ensure product id is empty to be assigned by the database
        // TODO : ensure the creation of the product with the correct category (document relationship)
        /// <summary>
        /// Create the Product document
        /// </summary>
        /// <returns> </returns>
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(ProductRequestModel newProduct)
        {
            try
            {
                await _productsService.CreateAsync(newProduct);
                if (string.IsNullOrEmpty(newProduct.Name) ||
                    string.IsNullOrEmpty(newProduct.Description) ||
                    newProduct.Price <= 0 ||
                    string.IsNullOrEmpty(newProduct.CategoryId) ||
                    string.IsNullOrEmpty(newProduct.Color))
                {
                    return BadRequest();
                }
                return Ok();
            }catch(Exception exception){
                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Return all the products for list porpouse
        /// </summary>
        /// <returns>List of products Summarized</returns>
        
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            try
            {
                var modelList = await _productsService.GetAllAsync(token);

                return Ok(modelList);
            }
            catch(Exception exception)
            {
                return BadRequest(exception);
            }
        }
        
        /// <summary>
        /// Returns an specific product selected by the Id
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
                var product = await _productsService.GetById(id);
                if (product == null)
                {
                    Console.WriteLine("Returning NotFound");
                    return NotFound();
                }

                Console.WriteLine("Returning Ok");
                return Ok(product);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return BadRequest(exception);
            }
        }
        
        /// <summary>
        /// Updates the Product fields based on the provided Id
        /// </summary>
        /// <returns> </returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryRequestModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]ProductRequestModel ProductToUpdate)
        {
            try
            {                
                await _productsService.UpdateAsync(id, ProductToUpdate);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete the Product document based on the provided Id
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
                await _productsService.RemoveAsync(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}