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

app.Use(async (context, next) =>
{
    // Libera GET sem autenticação
    if (context.Request.Method == "GET")
    {
        await next();
        return;
    }

    // Verifica API Key para POST, PUT, DELETE
    var apiKey = context.Request.Headers["API-Key"].ToString();

    if (apiKey != "minha-chave-api")
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Acesso negado",
            message = "API Key inválida ou não fornecida. Use o header: X-API-Key"
        });
        return;
    }

    await next();
});

var api = app.MapGroup("/api");


api.MapGroup("/city").MapCityEndpoints().WithTags("City");
//api.MapGroup("/address").MapAddressEndpoints().WithTags("Address");
//api.MapGroup("/actor").MapActorEndpoints().WithTags("Actor");
//api.MapGroup("/category").MapCategoryEndpoints().WithTags("Category");


app.Run();


