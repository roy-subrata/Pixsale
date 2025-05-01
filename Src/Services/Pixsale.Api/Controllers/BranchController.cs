using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Domain.Entities;
using Pixsale.Infrastructure;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController(
        ILogger<BranchController> logger,
        PixsaleDbContext dbContext) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Fetching all branches");
            return Ok(await dbContext.Branches.ToListAsync());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            logger.LogInformation("Fetching branch with ID: {id}", id);
            return Ok(await dbContext.Branches.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranch branch)
        {
            var newBranch = new Branch
            {
                Name = branch.Name,
                Description = branch.Description,
                Address = branch.Address,
            };
            await dbContext.Branches.AddAsync(newBranch);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Branch created with ID: {id}", newBranch.Id);
            return CreatedAtAction(nameof(GetById), new { newBranch.Id }, newBranch);
        }

        [HttpPut("id")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBranch branch)
        {
            logger.LogInformation("Updating branch with ID: {id}", id);
            if(id != branch.Id)
            {
                return BadRequest("Branch ID mismatch");
            }
            var existingBranch = await dbContext.Branches.FindAsync(branch.Id);
            if (existingBranch == null)
            {
                return NotFound();
            }
            existingBranch.Name = branch.Name;
            existingBranch.Description = branch.Description;
            existingBranch.Address = branch.Address;
            dbContext.Branches.Update(existingBranch);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingBranch = await dbContext.Branches.FindAsync(id);
            if (existingBranch == null)
            {
                return NotFound();
            }
            dbContext.Branches.Remove(existingBranch);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Branch with ID: {id} deleted", id);
            return NoContent();
        }
    }
}
