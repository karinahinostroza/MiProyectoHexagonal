using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetIdProductsAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new { message = "El ID del producto no coincide" });
            }

            var updated = await _productService.UpdateProductAsync(product);
            if (!updated)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }

            return Ok(new { message = "Producto actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound(new { message = "Producto no encontrado" });

            return NoContent(); // Código 204 si se eliminó correctamente
        }

    }
}
