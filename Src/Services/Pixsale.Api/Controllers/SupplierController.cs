using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Domain.Entities;
using Pixsale.Infrastructure;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController(
        ILogger<SupplierController> logger,
        PixsaleDbContext dbContext
        ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> FindAllAsync(string? name, int page=0, int pageSize=50)
        {
            var query = dbContext.Suppliers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.Name.ToLower().Contains(name.ToLower()));
            }

            var pagedSuppliers = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(pagedSuppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            return Ok(await dbContext.Suppliers.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplier supplier)
        {
            var newSupplier = new Supplier
            {
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address,
                City = supplier.City,
                State = supplier.State,
                Country = supplier.Country,
            };
            await dbContext.Suppliers.AddAsync(newSupplier);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Supplier with created with ID: {id}", newSupplier.Id);
            return CreatedAtAction(nameof(FindById), new { newSupplier.Id }, newSupplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplier supplier)
        {
            logger.LogInformation("Updating supplier with ID: {id}", id);
            if (id != supplier.Id)
            {
                return BadRequest("Supplier ID mismatch");
            }
            var existingSupplier = await dbContext.Suppliers.FindAsync(supplier.Id);
            if (existingSupplier == null)
            {
                return NotFound();
            }
            existingSupplier.Name = supplier.Name;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Phone = supplier.Phone;
            existingSupplier.Address = supplier.Address;
            existingSupplier.City = supplier.City;
            existingSupplier.State = supplier.State;
            existingSupplier.Country = supplier.Country;

            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Deleting supplier with ID: {id}", id);
            var supplier = await dbContext.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            dbContext.Suppliers.Remove(supplier);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
