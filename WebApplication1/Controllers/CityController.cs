using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresAPI.Models;
using System.Net;

namespace WebApplication1.Controllers
{
    public static class CityController
    {
        static async Task<Results<Ok<City>, BadRequest>> GetCityById([FromServices] AppDbContext context, int id)
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
    }

}
