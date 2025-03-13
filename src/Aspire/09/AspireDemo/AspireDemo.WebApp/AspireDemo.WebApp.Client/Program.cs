using AspireDemo.WebApp.Client.Pages.HowTo;
using Marqdouj.Html.Geolocation;
using Marqdouj.Html.Geolocation.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IGeolocationService, GeolocationService>();
builder.Services.AddScoped<CounterState>();

await builder.Build().RunAsync();
