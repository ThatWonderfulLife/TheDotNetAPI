using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresAPI.Models;


namespace WebApplication1.Controllers
{
    public static class AddressController
    {
        public static async Task<Results<Ok<Address>, BadRequest>> GetAddressById([FromServices] AppDbContext context, int id)
        {
            if (id <= 0)
            {
                return (TypedResults.BadRequest());
            }
            var address = await context.Address.FindAsync(id);

            if (address == null)
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(address);
        }
        public static async Task<Ok<List<Address>>> GetAllAddresses([FromServices] AppDbContext context)
        {
            var address = await context.Address.ToListAsync();
            return TypedResults.Ok(address);
        }

        public static async Task<Results<CreatedAtRoute, BadRequest>> CreateAddress([FromServices] AppDbContext context, [FromBody] Address address)
        {
            if (address == null)
            {
                return TypedResults.BadRequest();
            }
            context.Address.Add(address);
            await context.SaveChangesAsync();
            return TypedResults.CreatedAtRoute($"/api/address/{address.Id}", address);
        }

        public static async Task<Results<Ok, NotFound>> EditAddress([FromServices] AppDbContext context, int id, [FromBody] Address address)
        {
            var selectedAddress = await context.Address.FindAsync(id);
            if (selectedAddress == null)
            {
                return TypedResults.NotFound();
            }
            selectedAddress.Address1 = address.Address1;
            selectedAddress.Address2 = address.Address2;
            selectedAddress.District = address.District;
            selectedAddress.CityId = address.CityId;
            selectedAddress.PostalCode = address.PostalCode;
            selectedAddress.Phone = address.Phone;
            selectedAddress.LastUpdate = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, NotFound>> DeleteAddress([FromServices] AppDbContext context, int id)
        {
            var address = await context.Address.FindAsync(id);
            if (address == null)
            {
                return TypedResults.NotFound();
            }
            context.Address.Remove(address);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static RouteGroupBuilder MapAddressEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", AddressController.GetAllAddresses);
            group.MapGet("/{id}", AddressController.GetAddressById);
            group.MapPost("/", AddressController.CreateAddress);
            group.MapPut("/{id}", AddressController.EditAddress);
            group.MapDelete("/{id}", AddressController.DeleteAddress);
            return group;
        }

    }
}