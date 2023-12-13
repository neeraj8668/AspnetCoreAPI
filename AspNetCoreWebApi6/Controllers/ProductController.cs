
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductSevice _productService;

        public ProductsController(IProductSevice productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// method to get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllAsync();

            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }
        /// <summary>
        /// method to get single Product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productService.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// method to create new Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productService.CreateAsync(product);


            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        /// <summary>
        /// method to update existing product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productInput"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProduct(string id, Product productInput)
        {
            if (productInput is null)
            {
                return BadRequest();
            }

            var foundProduct = await _productService.GetAsync(id);

            foundProduct.Name = productInput.Name.Trim();

            if (foundProduct is null)
            {
                return NotFound();
            }
            await _productService.UpdateAsync(id, foundProduct);

            return NoContent();

        }
        /// <summary>
        /// method to delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var book = await _productService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _productService.RemoveAsync(id);

            return NoContent();
        }
    }
}
