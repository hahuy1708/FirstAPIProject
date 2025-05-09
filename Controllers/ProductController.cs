using Microsoft.AspNetCore.Mvc;
using FirstAPIProject.Models;

namespace FirstAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Price = 1000.00m, Category = "Electronics" },
            new Product { Id = 2, Name = "Desktop", Price = 2000.00m, Category = "Electronics" },
            new Product { Id = 3, Name = "Mobile", Price = 300.00m, Category = "Electronics" },
        };
        
        // Get: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }
        
        // Get: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if(product == null) return NotFound(new { Message = $"Product with ID {id} not found" });
            return Ok(product);
        }
        
        //Post: api/products
        [HttpPost]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        
        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, [FromBody] Product updated_Product)
        {
            if(id != updated_Product.Id) return BadRequest(new { Message = "ID mismatch between route and body." });
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if(existingProduct == null) return NotFound(new { Message = $"Product with ID {id} not found." });
            // update the product properties
            existingProduct.Name = updated_Product.Name;
            existingProduct.Price = updated_Product.Price;
            existingProduct.Category = updated_Product.Category;
            
            // In a real application, persist changes to the database here 
            
            return NoContent();  // HTTP status code 204
        }
        
        //Delete: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }
            _products.Remove(product);
            
            // In a real application, remove the product from the database here
            
            return NoContent();  // HTTP status code 204
        }
        
    }
}

