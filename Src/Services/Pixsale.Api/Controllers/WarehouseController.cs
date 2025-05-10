using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Domain.Entities;
using Pixsale.Infrastructure;

namespace Pixsale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController(
        ILogger<WarehouseController> logger,
        PixsaleDbContext dbContext) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Fetching all warehouses");
            return Ok(await dbContext.Warehouses.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            logger.LogInformation("Fetching warehouse with ID: {id}", id);
            var warehouse = await dbContext.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(warehouse);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouse warehouse)
        {
            var newWarehouse = new Warehouse
            {
                Name = warehouse.Name,
                Description = warehouse.Description,
                Address = warehouse.Address,
            };
            await dbContext.Warehouses.AddAsync(newWarehouse);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Warehouse created with ID: {id}", newWarehouse.Id);
            return CreatedAtAction(nameof(Get), new { newWarehouse.Id }, newWarehouse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouse warehouse)
        {
            logger.LogInformation("Updating warehouse with ID: {id}", id);
            if (id != warehouse.Id)
            {
                return BadRequest("Warehouse ID mismatch");
            }
            var existingWarehouse = await dbContext.Warehouses.FindAsync(warehouse.Id);
            if (existingWarehouse == null)
            {
                return NotFound();
            }
            existingWarehouse.Name = warehouse.Name;
            existingWarehouse.Description = warehouse.Description;
            existingWarehouse.Address = warehouse.Address;
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Deleting warehouse with ID: {id}", id);
            var warehouse = await dbContext.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            dbContext.Warehouses.Remove(warehouse);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
