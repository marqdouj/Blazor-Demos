var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

// https://learn.microsoft.com/en-us/dotnet/aspire/extensibility/secure-communication-between-integrations
// For this demo the values are placed in appsettings.json.
// Normally, you would use a secret manager to store these values
var mailDevUsername = builder.AddParameter("maildev-username");
var mailDevPassword = builder.AddParameter("maildev-password");
var maildev = builder.AddMailDev(
    name: "maildev",
    userName: mailDevUsername,
    password: mailDevPassword);

var apiService = builder.AddProject<Projects.AspireDemo_ApiService>("apiservice")
    .WithReference(maildev)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.AspireDemo_WebApp>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReference(maildev)
    .WithHttpHealthCheck("/health");

builder.Build().Run();
