using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresAPI.Models;
using System.Diagnostics.Metrics;
using System.Net;

namespace WebApplication1.Controllers
{
    public static class CountryController
    {
        public static async Task<Results<Ok<Country>, BadRequest>> GetCountryById([FromServices] AppDbContext context, int id)
        {
            if (id <= 0)
            {
                return (TypedResults.BadRequest());
            }
            var country = await context.Country.FindAsync(id);

            if (country == null)
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(country);
        }

        public static async Task<Results<Ok<List<Country>>, BadRequest>> GetAllCountries([FromServices] AppDbContext context)
        {
            var countries = await context.Country.ToListAsync();
            return TypedResults.Ok(countries);
        }

        public static async Task<Results<CreatedAtRoute, BadRequest>> CreateCountry([FromServices] AppDbContext context, [FromBody] Country country)
        {
            if (country == null)
            {
                return TypedResults.BadRequest();
            }
            context.Country.Add(country);
            await context.SaveChangesAsync();
            return TypedResults.CreatedAtRoute($"/api/cities/{country.Id}", country);
        }

        public static async Task<Results<Ok, NotFound>> EditCountry([FromServices] AppDbContext context, int id, [FromBody] Country country)
        {
            var selectedCountry = await context.Country.FindAsync(id);
            if (selectedCountry == null)
            {
                return TypedResults.NotFound();
            }
            selectedCountry.countryName = country.countryName;
            selectedCountry.lastUpdate = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, NotFound>> DeleteCountry([FromServices] AppDbContext context, int id)
        {
            var country = await context.Country.FindAsync(id);
            if (country == null)
            {
                return TypedResults.NotFound();
            }
            context.Country.Remove(country);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }
    }
}