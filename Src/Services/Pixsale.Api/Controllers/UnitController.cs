using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Domain.Entities;
using Pixsale.Infrastructure;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController(
        ILogger<UnitController> logger,
        PixsaleDbContext dbContext
        ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.Units.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await dbContext.Units.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUnit Unit)
        {
            var newUnit = new Unit
            {
                Name = Unit.Name,
                ShortName = Unit.ShortName,
                ConversionFactor = Unit.ConversionFactor
            };
            await dbContext.Units.AddAsync(newUnit);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Unit created with ID: {id}", newUnit.Id);
            return CreatedAtAction(nameof(Get), new { newUnit.Id }, newUnit);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUnit Unit)
        {
            logger.LogInformation("Updating Unit with ID: {id}", id);
            if (id != Unit.Id)
            {
                return BadRequest("Unit ID mismatch");
            }
            var existingUnit = await dbContext.Units.FindAsync(Unit.Id);
            if (existingUnit == null)
            {
                return NotFound();
            }
            existingUnit.Name = Unit.Name;
            existingUnit.ShortName = Unit.ShortName;
            existingUnit.ConversionFactor = Unit.ConversionFactor;
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Deleting Unit with ID: {id}", id);
            var Unit = await dbContext.Units.FindAsync(id);
            if (Unit == null)
            {
                return NotFound();
            }
            dbContext.Units.Remove(Unit);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
