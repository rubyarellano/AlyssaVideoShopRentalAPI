using AlyssaVideoShopRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlyssaVideoShopRentalAPI.Data;

namespace AlyssaVideoShopRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly VideoShopDbContext DBcontext;

        public CustomersController(VideoShopDbContext context)
        {
            DBcontext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await DBcontext.Customers
                .Include(c => c.RentalHeaders)
                .ThenInclude(rh => rh.RentalDetails)
                .ThenInclude(rd => rd.Movies)
                .ToListAsync();
            return Ok(customers);
        }

        [HttpGet("id={id}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomerById(int id)
        {
            var customer = DBcontext.Customers
                .Include(c => c.RentalHeaders)
                .ThenInclude(rh => rh.RentalDetails)
                .ThenInclude(rd => rd.Movies)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddStudent([FromBody] Customer customer)
        {
            DBcontext.Customers.Add(customer);
            await DBcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> UpdateCustomerDetails(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            DBcontext.Entry(customer).State = EntityState.Modified;

            try
            {
                await DBcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DBcontext.Customers.Any(c => c.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = DBcontext.Customers.Find(id);
            if (customer == null)
                return NotFound();

            DBcontext.Customers.Remove(customer);
            await DBcontext.SaveChangesAsync();

            return NoContent();
        }
    }
}