using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddDataAccess(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")))
       .AddYandexClient();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
