using AspireDemo.Components.Other.Canvas;
using AspireDemo.WebApp.Client.Pages.HowTo;
using Marqdouj.Html.Geolocation;
using Marqdouj.Html.Geolocation.Models;
using Marqdouj.Html.ResizeObserver;
using Marqdouj.JSLogger;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddFluentUIComponents();

/*
For the purpose of this demo, both type of loggers are configured. 
normally you would only configure one type of logger service.
*/
builder.Services.AddLoggerModule(null);
builder.Services.AddLoggerService(null);

builder.Services.AddScoped<ResizeObserverService>();
builder.Services.AddScoped<IGeolocationService, GeolocationService>();
builder.Services.AddScoped<CanvasDemoState>();
builder.Services.AddScoped<CounterState>();

await builder.Build().RunAsync();
