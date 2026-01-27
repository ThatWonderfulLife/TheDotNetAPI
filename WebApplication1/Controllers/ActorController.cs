using Microsoft.AspNetCore.Mvc;
using PostgresAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetActors")]
        public async Task<IActionResult> GetActors() {
            var result = await _context.Actor.Select(x => new Actor
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                LastUpdate = x.LastUpdate
            }).ToListAsync();

            return Ok(result);
        }


        [HttpGet("GetActorById")]
        public async Task<IActionResult> GetActorById(int actorId)
        {
            var actor = await _context.Actor.Where(x => x.Id == actorId).Select(x => new Actor
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                LastUpdate = x.LastUpdate
            }).FirstOrDefaultAsync();

            if (actor == null)
                return NotFound();

            return Ok(actor);
        }

        [HttpPost("CreateActor")]
        public async Task<IActionResult> CreateActor([FromBody] Actor actor)
        {
            _context.Actor.Add(actor);
            await _context.SaveChangesAsync();

            return Ok(actor);
        }

        [HttpPut("EditActor")]
        public async Task<IActionResult> EditActor([FromBody] Actor actor)
        {
            var rows = await _context.Actor.Where(x => x.Id == actor.Id)
                .ExecuteUpdateAsync(update => update
                .SetProperty(x => x.FirstName, actor.FirstName)
                .SetProperty(x => x.LastName , actor.LastName)
                .SetProperty(x => x.LastUpdate,DateTime.UtcNow)
                );
            if (rows == 0)
                return NotFound();

            return Ok(actor);
        }

        [HttpDelete("DeleteActor")]
        public async Task<IActionResult> DeleteActor (int actorID)
        {
            var rows = await _context.Actor.Where(x => x.Id == actorID).ExecuteDeleteAsync();

            if (rows == 0)
                return NotFound();

            return Ok(true);
        }

    }
}
