using AspireDemo.ApiClient;
using AspireDemo.WebApp.Client.Pages.HowTo;
using AspireDemo.WebApp.Components;
using Marqdouj.Html.Geolocation;
using Marqdouj.Html.Geolocation.Models;
using Marqdouj.Html.ResizeObserver;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddHttpClient<IApiServiceClient, ApiServiceClient>(client =>
{
    //For Aspire (not yet supported in .NET 10) client.BaseAddress = new("https+http://apiservice");
    client.BaseAddress = new("https://localhost:7093");
});

builder.Services.AddScoped<ResizeObserverService>();
builder.Services.AddScoped<IGeolocationService, GeolocationService>();
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

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AspireDemo.WebApp.Client._Imports).Assembly);

app.Run();
