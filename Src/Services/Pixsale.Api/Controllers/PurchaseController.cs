using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pixsale.Application.Models;
using Pixsale.Infrastructure;

namespace Pixsale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController(
        ILogger<PurchaseController> logger,
        PixsaleDbContext dbContext
        ) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Fetching all purchases from the database.");
            var purchases = await dbContext.Purchases.ToListAsync();
            return Ok(purchases);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            logger.LogInformation($"Fetching purchase with ID {id} from the database.");
            var purchase = await dbContext.Purchases.FindAsync(id);
            if (purchase == null)
            {
                logger.LogWarning($"Purchase with ID {id} not found.");
                return NotFound();
            }
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchase purchase)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid purchase model state.");
                return BadRequest(ModelState);
            }
            var newPurchase = new Domain.Entities.Purchase()
            {
                PurchaseDate = purchase.PurchaseDate,
                SupplierId = purchase.SupplierId,
                SubTotal = 0,
                TotalVAT = 0,
                TotalAmount = 0,
                Status = Domain.Entities.PurchaseStatus.Quotation,
                Notes = string.Empty
            };
            dbContext.Purchases.Add(newPurchase);

            dbContext.PurchaseDetails.AddRange(purchase.PurchaseDetails.Select(pd => new Domain.Entities.PurchaseDetail
            {
                ProductId = pd.ProductId,
                Quantity = pd.Quantity,
                UnitPrice = pd.UnitPrice,
                VATAmount = pd.VATAmount,
                PurchaseId = newPurchase.Id
            }));
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Purchase with ID {newPurchase.Id} created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = newPurchase.Id }, newPurchase);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePurchase purchase)
        {
            if (id != purchase.Id)
            {
                logger.LogWarning($"Purchase ID mismatch: {id} != {purchase.Id}");
                return BadRequest();
            }

            var existingPurchase = await dbContext.Purchases
                .Include(p => p.PurchaseDetails)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingPurchase == null)
            {
                logger.LogWarning($"Purchase with ID {id} not found.");
                return NotFound();
            }

            // Update purchase properties
            existingPurchase.PurchaseDate = purchase.PurchaseDate;
            existingPurchase.SupplierId = purchase.SupplierId;
            existingPurchase.Status = (Domain.Entities.PurchaseStatus)purchase.Status; // Explicit cast added


            // Update PurchaseDetails
            var updatedDetails = purchase.PurchaseDetails.Select(pd => new Domain.Entities.PurchaseDetail
            {
                Id = pd.Id,
                ProductId = pd.ProductId,
                Quantity = pd.Quantity,
                UnitPrice = pd.UnitPrice,
                VATAmount = pd.VATAmount,
                PurchaseId = id
            }).ToList();

            // Remove deleted details
            var detailsToRemove = existingPurchase.PurchaseDetails
                .Where(ed => !updatedDetails.Any(ud => ud.Id == ed.Id))
                .ToList();
            dbContext.PurchaseDetails.RemoveRange(detailsToRemove);

            // Add or update details
            foreach (var updatedDetail in updatedDetails)
            {
                var existingDetail = existingPurchase.PurchaseDetails
                    .FirstOrDefault(ed => ed.Id == updatedDetail.Id);

                if (existingDetail == null)
                {
                    dbContext.PurchaseDetails.Add(updatedDetail);
                }
                else
                {
                    existingDetail.ProductId = updatedDetail.ProductId;
                    existingDetail.Quantity = updatedDetail.Quantity;
                    existingDetail.UnitPrice = updatedDetail.UnitPrice;
                    existingDetail.VATAmount = updatedDetail.VATAmount;
                }
            }

            try
            {
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Purchase with ID {id} updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException($"Purchase with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var purchase = await dbContext.Purchases.FindAsync(id);
            if (purchase == null)
            {
                logger.LogWarning($"Purchase with ID {id} not found for deletion.");
                return NotFound();
            }
            dbContext.Purchases.Remove(purchase);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Purchase with ID {id} deleted successfully.");
            return NoContent();
        }
    }
}
