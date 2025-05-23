using AspireDemo.ApiClient;
using AspireDemo.Components.Other.Canvas;
using AspireDemo.WebApp.Client.Pages.HowTo;
using AspireDemo.WebApp.Components;
using Marqdouj.Html.Geolocation;
using Marqdouj.Html.Geolocation.Models;
using Marqdouj.Html.ResizeObserver;
using Marqdouj.JSLogger;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOutputCache();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

/*
For the purpose of this demo, both type of loggers are configured. 
normally you would only configure one type of logger service.
*/
builder.AddLoggerModule(null);
builder.AddLoggerService(null); /*See `App.Razor` for how to add the global script required for this service */

builder.Services.AddFluentUIComponents();

builder.Services.AddHttpClient<IApiServiceClient, ApiServiceClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://apiservice");
});

builder.Services.AddScoped<ResizeObserverService>();
builder.Services.AddScoped<IGeolocationService, GeolocationService>();
builder.Services.AddScoped<CanvasDemoState>();
builder.Services.AddScoped<CounterState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AspireDemo.WebApp.Client._Imports).Assembly);

app.MapDefaultEndpoints();

app.Run();
