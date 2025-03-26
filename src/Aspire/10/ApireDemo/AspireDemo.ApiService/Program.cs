using AspireDemo.ApiService.Endpoints;
using AspireDemo.ApiService.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.ConfigureEmailService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.DefaultFonts = false; // Disable default fonts to avoid download unnecessary fonts
        //options.Servers = []; //Required in Aspire
    });
}

app.UseHttpsRedirection();

app.MapNewsletterApi();
app.MapWeatherApi();

app.Run();
