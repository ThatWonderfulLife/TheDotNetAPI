using Microsoft.EntityFrameworkCore;
using System;
using PostgresAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

static async Task<Results<Ok<City>,BadRequest>> getCityById([FromServices] AppDbContext context, int id)
{
    if(id <= 0)
    {
        return (TypedResults.BadRequest());
    }
    var cities = await context.City.FindAsync(id);
    return TypedResults.Ok(cities);
}

app.MapGet("/city/{id}",getCityById);

app.Run();


