using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresAPI.Models;
using System.Net;
using System.Numerics;

namespace WebApplication1.Controllers
{
    public static class ActorController
    {
        public static async Task<Results<Ok<Actor>, BadRequest>> GetActorById([FromServices] AppDbContext context, int id)
        {
            if (id <= 0)
            {
                return (TypedResults.BadRequest());
            }
            var actor = await context.Actor.FindAsync(id);

            if (actor == null)
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(actor);
        }
        public static async Task<Ok<List<Actor>>> GetAllActor([FromServices] AppDbContext context)
        {
            var actor = await context.Actor.ToListAsync();
            return TypedResults.Ok(actor);
        }

        public static async Task<Results<CreatedAtRoute, BadRequest>> CreateActor([FromServices] AppDbContext context, [FromBody] Actor actor)
        {
            if (actor == null)
            {
                return TypedResults.BadRequest();
            }
            context.Actor.Add(actor);
            await context.SaveChangesAsync();
            return TypedResults.CreatedAtRoute($"/api/actor/{actor.Id}", actor);
        }

        public static async Task<Results<Ok, NotFound>> EditActor([FromServices] AppDbContext context, int id, [FromBody] Actor actor)
        {
            var selectedActor = await context.Actor.FindAsync(id);
            if (selectedActor == null)
            {
                return TypedResults.NotFound();
            }
            selectedActor.FirstName = actor.FirstName;
            selectedActor.LastName = actor.LastName;
            selectedActor.LastUpdate = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, NotFound>> DeleteActor([FromServices] AppDbContext context, int id)
        {
            var actor = await context.Actor.FindAsync(id);
            if (actor == null)
            {
                return TypedResults.NotFound();
            }
            context.Actor.Remove(actor);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static RouteGroupBuilder MapActorEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", ActorController.GetAllActor);
            group.MapGet("/{id}", ActorController.GetActorById);
            group.MapPost("/", ActorController.CreateActor);
            group.MapPut("/{id}", ActorController.EditActor);
            group.MapDelete("/{id}", ActorController.DeleteActor);
            return group;
        }
    }
}