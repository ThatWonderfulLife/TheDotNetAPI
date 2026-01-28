using Microsoft.AspNetCore.Mvc;
using PostgresAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAddresses")]
        public async Task<IActionResult> GetAddresses()
        {
            var result = await _context.Address.Select(x => new Address
            {
                Id = x.Id,
                Address1 = x.Address1,
                Address2 = x.Address2,
                District = x.District,
                CityId = x.CityId,
                PostalCode = x.PostalCode,
                Phone = x.Phone,
                LastUpdate = x.LastUpdate
            }).ToListAsync();

            return Ok(result);
        }


        [HttpGet("GetAddressById")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var address = await _context.Address.Where(x => x.Id == addressId).Select(x => new Address
            {
                Id = x.Id,
                Address1 = x.Address1,
                Address2 = x.Address2,
                District = x.District,
                CityId = x.CityId,
                PostalCode = x.PostalCode,
                Phone = x.Phone,
                LastUpdate = x.LastUpdate
            }).FirstOrDefaultAsync();

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {

            if(address == null)
                return BadRequest("Invalid Request");

            address.LastUpdate = DateTime.UtcNow;
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(CreateAddress),
                new { id = address.Id },
                address
            );
        }

        [HttpPut("EditAddress")]
        public async Task<IActionResult> EditAddress([FromBody] Address address)
        {
            var rows = await _context.Address.Where(x => x.Id == address.Id)
                .ExecuteUpdateAsync(update => update
                .SetProperty(x => x.Address1, address.Address1)
                .SetProperty(x => x.Address2, address.Address2)
                .SetProperty(x => x.District, address.District)
                .SetProperty(x => x.CityId, address.CityId)
                .SetProperty(x => x.PostalCode, address.PostalCode)
                .SetProperty(x => x.Phone, address.Phone)
                .SetProperty(x => x.LastUpdate, DateTime.UtcNow)
                );
            if (rows == 0)
                return NotFound();

            return Ok(rows);
        }

        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var rows = await _context.Address.Where(x => x.Id == addressId).ExecuteDeleteAsync();

            if (rows == 0)
                return NotFound();

            return Ok(true);
        }

    }
}
