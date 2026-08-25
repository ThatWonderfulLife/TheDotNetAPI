using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresAPI.Models;
using System.Net;

namespace WebApplication1.Controllers
{
    public static class CategoryController
    {
        public static async Task<Results<Ok<Category>, BadRequest>> GetCategoryById([FromServices] AppDbContext context, int id)
        {
            if (id <= 0)
            {
                return (TypedResults.BadRequest());
            }
            var category = await context.Category.FindAsync(id);

            if (category == null)
            {
                return TypedResults.BadRequest();
            }

            return TypedResults.Ok(category);
        }
        public static async Task<Ok<List<Category>>> GetAllCategories([FromServices] AppDbContext context)
        {
            var category = await context.Category.ToListAsync();
            return TypedResults.Ok(category);
        }

        public static async Task<Results<CreatedAtRoute, BadRequest>> CreateCategory([FromServices] AppDbContext context, [FromBody] Category category)
        {
            if (category == null)
            {
                return TypedResults.BadRequest();
            }
            context.Category.Add(category);
            await context.SaveChangesAsync();
            return TypedResults.CreatedAtRoute($"/api/category/{category.Id}", category);
        }

        public static async Task<Results<Ok, NotFound>> EditCategory([FromServices] AppDbContext context, int id, [FromBody] Category category)
        {
            var selectedCategory = await context.Category.FindAsync(id);
            if (selectedCategory == null)
            {
                return TypedResults.NotFound();
            }
            selectedCategory.Name = category.Name;
            selectedCategory.LastUpdate = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, NotFound>> DeleteCategory([FromServices] AppDbContext context, int id)
        {
            var category = await context.Category.FindAsync(id);
            if (category == null)
            {
                return TypedResults.NotFound();
            }
            context.Category.Remove(category);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", CategoryController.GetAllCategories);
            group.MapGet("/{id}", CategoryController.GetCategoryById);
            group.MapPost("/", CategoryController.CreateCategory);
            group.MapPut("/{id}", CategoryController.EditCategory);
            group.MapDelete("/{id}", CategoryController.DeleteCategory);
            return group;
        }

    }
}
