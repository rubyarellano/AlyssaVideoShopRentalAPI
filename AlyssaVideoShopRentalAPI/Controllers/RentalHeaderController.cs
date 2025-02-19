using AlyssaVideoShopRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlyssaVideoShopRentalAPI.Data;


namespace AlyssaVideoShopRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalHeadersController : ControllerBase
    {
        private readonly VideoShopDbContext _context;

        public RentalHeadersController(VideoShopDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<RentalHeader>> PostRentalHeader(RentalHeader rentalHeader)
        {
            rentalHeader.RentalDate = DateTime.Now;
            _context.RentalHeaders.Add(rentalHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRentalHeader), new { id = rentalHeader.CustomerId }, rentalHeader);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalHeader>>> GetRentalHeader()
        {
            if (_context.RentalHeaders == null)
            {

                return NotFound();
            }
            return await _context.RentalHeaders.ToListAsync();
        }


        [HttpPut]
        public async Task<ActionResult> PutRentalHeader(int id, RentalHeader rentalHeader)
        {
            if (id != rentalHeader.CustomerId)
            {
                return BadRequest();
            }
            _context.Entry(rentalHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalHeaderAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }
        private bool RentalHeaderAvailable(int id)
        {
            return (_context.RentalHeaders?.Any(x => x.CustomerId == id)).GetValueOrDefault();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalHeader>> GetRentalHeader(int id)
        {
            {
                return NotFound();
            }
            var rentalHeader = await _context.RentalHeaders.FindAsync(id);
            if (rentalHeader == null)
            {
                return NotFound();

            }
            return rentalHeader;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.RentalHeaders == null)
            {
                return NotFound();
            }
            var rentalHeader = await _context.RentalHeaders.FindAsync(id);
            if (rentalHeader == null)
            {
                return NotFound();
            }
            _context.RentalHeaders.Remove(rentalHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

