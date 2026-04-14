using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresAPI.Models;


namespace WebApplication1.Controllers
{
    public static class CityController
    {
        public static async Task<Results<Ok<City>, BadRequest>> GetCityById([FromServices] AppDbContext context, int id)
        {
            if (id <= 0)
            {
                return (TypedResults.BadRequest());
            }
            var city = await context.City.FindAsync(id);

            if (city == null)
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(city);
        }
        public static async Task<Ok<List<City>>> GetAllCities([FromServices] AppDbContext context)
        {
            var cities = await context.City.ToListAsync();
            return TypedResults.Ok(cities);
        }

        public static async Task<Results<CreatedAtRoute, BadRequest>> CreateCity([FromServices] AppDbContext context, [FromBody] City city)
        {
            if (city == null)
            {
                return TypedResults.BadRequest();
            }
            context.City.Add(city);
            await context.SaveChangesAsync();
            return TypedResults.CreatedAtRoute($"/api/city/{city.Id}", city);
        }

        public static async Task<Results<Ok, NotFound>> EditCity([FromServices] AppDbContext context, int id, [FromBody] City city)
        {
            var selectedCity = await context.City.FindAsync(id);
            if (selectedCity == null)
            {
                return TypedResults.NotFound();
            }
            selectedCity.CityName = city.CityName;
            selectedCity.LastUpdate = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, NotFound>> DeleteCity([FromServices] AppDbContext context, int id)
        {
            var city = await context.City.FindAsync(id);
            if (city == null)
            {
                return TypedResults.NotFound();
            }
            context.City.Remove(city);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok<List<City>>, BadRequest>> GetCitiesByCountryId([FromServices] AppDbContext context, int id)
        {
            if (id <= 0)
            {
                return (TypedResults.BadRequest());
            }
            var cities = await context.City.Where(c => c.CountryId == id).ToListAsync();

            if (cities == null)
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(cities);
        }

        public static RouteGroupBuilder MapCityEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", CityController.GetAllCities);
            group.MapGet("/{id}", CityController.GetCityById);
            group.MapPost("/", CityController.CreateCity);
            group.MapPut("/{id}", CityController.EditCity);
            group.MapDelete("/{id}", CityController.DeleteCity);
            return group;
        }
    }

}
