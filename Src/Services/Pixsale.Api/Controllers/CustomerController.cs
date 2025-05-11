using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Domain.Entities;
using Pixsale.Infrastructure;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(
        ILogger<CustomerController> logger,
        PixsaleDbContext dbContext
        ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.Customers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await dbContext.Customers.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomer customer)
        {
            var newCustomer = new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                NationalId = customer.NationalId,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                Country = customer.Country,
                Gender = customer.Gender,
                ZipCode = customer.ZipCode
            };
            await dbContext.Customers.AddAsync(newCustomer);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Customer created with ID: {id}", newCustomer.Id);
            return CreatedAtAction(nameof(Get), new { newCustomer.Id }, newCustomer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomer customer)
        {
            logger.LogInformation("Updating customer with ID: {id}", id);
            if (id != customer.Id)
            {
                return BadRequest("Customer ID mismatch");
            }
            var existingCustomer = await dbContext.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.NationalId = customer.NationalId;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            existingCustomer.State = customer.State;
            existingCustomer.Country = customer.Country;
            existingCustomer.Gender = customer.Gender;
            existingCustomer.ZipCode = customer.ZipCode;

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation("Deleting customer with ID: {id}", id);
            var customer = await dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
