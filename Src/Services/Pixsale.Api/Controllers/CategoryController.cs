using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Domain.Entities;
using Pixsale.Infrastructure;
using Category = Pixsale.Domain.Entities.Category;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(
        ILogger<CategoryController> logger,
        PixsaleDbContext dbContext
        ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.Categories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await dbContext.Categories.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategory Category)
        {
            var newCategory = new Category
            {
                Name = Category.Name,
                Description = Category.Description,
                ParentId = Category.ParentId
            };
            await dbContext.Categories.AddAsync(newCategory);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Category created with ID: {id}", newCategory.Id);
            return CreatedAtAction(nameof(Get), new { newCategory.Id }, newCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategory Category)
        {
            logger.LogInformation("Updating Category with ID: {id}", id);
            if (id != Category.Id)
            {
                return BadRequest("Category ID mismatch");
            }
            var existingCategory = await dbContext.Categories.FindAsync(Category.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.Name = Category.Name;
            existingCategory.Description = Category.Description;
            existingCategory.ParentId = Category.ParentId;

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Deleting Category with ID: {id}", id);
            var Category = await dbContext.Categories.FindAsync(id);
            if (Category == null)
            {
                return NotFound();
            }
            dbContext.Categories.Remove(Category);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
