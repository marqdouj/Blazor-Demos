using AspireDemo.Components.Other.Canvas;
using AspireDemo.WebApp.Client.Pages.HowTo;
using Marqdouj.Html.Geolocation;
using Marqdouj.Html.Geolocation.Models;
using Marqdouj.Html.ResizeObserver;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<ResizeObserverService>();
builder.Services.AddScoped<IGeolocationService, GeolocationService>();
builder.Services.AddScoped<CanvasDemoState>();
builder.Services.AddScoped<CounterState>();

await builder.Build().RunAsync();
