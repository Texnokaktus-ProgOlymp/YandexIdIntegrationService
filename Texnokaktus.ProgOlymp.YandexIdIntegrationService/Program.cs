using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services.Abstractions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddDataAccess(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")))
       .AddYandexClient()
       .AddLogic();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

await app.RunAsync();
