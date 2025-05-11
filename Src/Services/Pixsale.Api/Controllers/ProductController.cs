using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Domain.Entities;
using Pixsale.Infrastructure;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(
        ILogger<ProductController> logger,
        PixsaleDbContext dbContext
        ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await dbContext.Products.
                
                Include(p => p.Category)
                .Include(p=>p.Unit)
                .Select(product => new GetProduct(
                    product.Id,
                    product.Name,
                    product.LocalName,
                    product.Brand,
                    product.Description,
                    new EntityRef(product.Category.Id, product.Category.Name),
                    new EntityRef(product.Unit.Id, product.Unit.Name),
                    product.TotalQty
                    )).ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await dbContext.Products.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProduct Product)
        {
            var newProduct = new Product()
            {
                Name = Product.Name,
                LocalName = Product.LocalName,
                Brand = Product.Brand,
                Description = Product.Description,
                UnitId = Product.UnitId,
                CategoryId = Product.CategoryId,
            };
            await dbContext.Products.AddAsync(newProduct);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Product created with ID: {id}", newProduct.Id);
            return CreatedAtAction(nameof(Get), new { newProduct.Id }, newProduct);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProduct Product)
        {
            logger.LogInformation("Updating Product with ID: {id}", id);
            if (id != Product.Id)
            {
                return BadRequest("Product ID mismatch");
            }
            var existingProduct = await dbContext.Products.FindAsync(Product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = Product.Name;
            existingProduct.LocalName = Product.LocalName;
            existingProduct.Brand=Product.Brand;
            existingProduct.UnitId=Product.UnitId;
            existingProduct.Description = Product.Description;
            existingProduct.CategoryId = Product.CategoryId;
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Deleting Product with ID: {id}", id);
            var Product = await dbContext.Products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            dbContext.Products.Remove(Product);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
