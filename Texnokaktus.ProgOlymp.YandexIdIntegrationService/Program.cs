using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Grpc;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddDataAccess(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")))
       .AddYandexClient()
       .AddLogic();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<UserServiceImpl>();

await app.RunAsync();
