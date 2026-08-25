using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresAPI.Models;
using System.Net;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllData")]
        public async Task<ActionResult<Sensor[]>> GetAllData()  
        {
            var result = await _context.Sensor.ToArrayAsync();

            return Ok(result);
        }


        [HttpGet("GetDataById")]
        public async Task<IActionResult> GetDataById(int sensorId)
        {
            var sensorData = await _context.Sensor.Where(x => x.Id == sensorId).FirstOrDefaultAsync();

            if (sensorData == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Invalid Request");

            category.LastUpdate = DateTime.UtcNow;
            _context.Category.Add(category);
            await _context.SaveChangesAsync();


            return CreatedAtAction(
                nameof(CreateCategory),
                new { id = category.Id },
                category
                );
        }

        [HttpPut("EditCategory")]
        public async Task<IActionResult> EditCategory([FromBody] Category category)
        {
            var rows = await _context.Category.Where(x => x.Id == category.Id)
                .ExecuteUpdateAsync(update => update
                .SetProperty(x => x.Name, category.Name)
                .SetProperty(x => x.LastUpdate, DateTime.UtcNow)
                );
            if (rows == 0)
                return NotFound();

            return Ok(category);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var rows = await _context.Category.Where(x => x.Id == categoryId).ExecuteDeleteAsync();

            if (rows == 0)
                return NotFound();

            return Ok(true);
        }

    }
}
