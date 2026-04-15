using Microsoft.EntityFrameworkCore;
using System;
using PostgresAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using WebApplication1.Controllers;
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

var api = app.MapGroup("/api");


api.MapGroup("/city").MapCityEndpoints().WithTags("City");
//api.MapGroup("/address").MapAddressEndpoints().WithTags("Address");
//api.MapGroup("/actor").MapActorEndpoints().WithTags("Actor");
//api.MapGroup("/category").MapCategoryEndpoints().WithTags("Category");


app.Run();


