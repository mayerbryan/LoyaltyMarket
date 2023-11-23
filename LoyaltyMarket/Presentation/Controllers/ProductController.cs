using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using Domain.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Domain.Models.ProductModels;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productsService;
        public ProductController(IProductService ProductsService) =>
            _productsService = ProductsService;

        /// <summary>
        /// Create the Product document
        /// </summary>
        /// <returns> Return ok on product creation successfully</returns>
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(ProductRequestModel newProduct, CancellationToken cancellationToken  = default)
        {
            try
            {
                await _productsService.CreateAsync(newProduct, cancellationToken);
                
                if (string.IsNullOrEmpty(newProduct.Name) ||
                    string.IsNullOrEmpty(newProduct.Description) ||
                    newProduct.Price <= 0 ||
                    string.IsNullOrEmpty(newProduct.CategoryId) ||
                    string.IsNullOrEmpty(newProduct.Color) || 
                    newProduct.Name.Length > 100 || 
                    newProduct.Description.Length > 150)
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
        /// <returns>Return all the products encountered in the collection</returns>        
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var modelList = await _productsService.GetAllAsync(cancellationToken);

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
        /// <returns> Return a especific product using the given Id </returns>
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productsService.GetById(id, cancellationToken);
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
        /// <returns> Updates the product using the given Id and returns Ok when successfull </returns>
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]ProductRequestModel ProductToUpdate, CancellationToken cancellationToken)
        {
            try
            {                
                await _productsService.UpdateAsync(id, ProductToUpdate, cancellationToken);
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
        /// <returns> Returns ok when the product is deleted successfully </returns>
        [ProducesResponseType(typeof(IEnumerable<ProductResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                await _productsService.RemoveAsync(id, cancellationToken);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}