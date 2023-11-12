
// using Microsoft.AspNetCore.Mvc;

// namespace Presentation.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ProductController : ControllerBase
//     {
//         private readonly ProductService _ProductsService;

//         public ProductController(ProductService ProductsService) =>
//             _ProductsService = ProductsService;

//         [HttpGet]
//         public async Task<List<Product>> Get() =>
//             await _ProductsService.GetAllAsync();

//         [HttpGet("{id:length(24)}")]
//         public async Task<ActionResult<Product>> Get(string id)
//         {
//             var Product = await _ProductsService.GetByIdAsync(id);

//             if (Product is null)
//             {
//                 return NotFound();
//             }

//             return Product;
//         }

//         [HttpPost]
//         public async Task<IActionResult> Post(Product newProduct)
//         {
//             newProduct.CategoryName = null;
//             await _ProductsService.CreateAsync(newProduct);

//             return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
//         }

//         [HttpPut("{id:length(24)}")]
//         public async Task<IActionResult> Update(string id, Product updatedProduct)
//         {
//             updatedProduct.CategoryName = null;
//             var Product = await _ProductsService.GetByIdAsync(id);

//             if (Product is null)
//             {
//                 return NotFound();
//             }

//             updatedProduct.Id = Product.Id;

//             await _ProductsService.UpdateAsync(id, updatedProduct);

//             return NoContent();
//         }

//         [HttpDelete("{id:length(24)}")]
//         public async Task<IActionResult> Delete(string id)
//         {
//             var Product = await _ProductsService.GetByIdAsync(id);

//             if (Product is null)
//             {
//                 return NotFound();
//             }

//             await _ProductsService.RemoveAsync(id);

//             return NoContent();
//         }
//     }
// }