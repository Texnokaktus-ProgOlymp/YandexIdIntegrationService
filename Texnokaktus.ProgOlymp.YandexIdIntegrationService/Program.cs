using System.Reflection;
using Microsoft.AspNetCore.DataProtection;
using Serilog;
using StackExchange.Redis;
using Texnokaktus.ProgOlymp.OpenTelemetry;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Abstractions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Grpc;
using YandexOAuthClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOAuthClient()
       .AddHttpClient<IYandexIdClient, YandexIdClient>(client => client.BaseAddress = new("https://login.yandex.ru"));

var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(builder.Configuration.GetConnectionString("DefaultRedis")!);
builder.Services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
builder.Services.AddStackExchangeRedisCache(options => options.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(connectionMultiplexer));
builder.Services.AddMemoryCache();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom
                                                                 .Configuration(context.Configuration)
                                                                 .AddOpenTelemetrySupport("YandexIdIntegrationService"));

builder.Services.AddTexnokaktusOpenTelemetry("YandexIdIntegrationService", null, null);

builder.Services
       .AddDataProtection(options => options.ApplicationDiscriminator = Assembly.GetEntryAssembly()?.GetName().Name)
       .PersistKeysToStackExchangeRedis(connectionMultiplexer)
       .DisableAutomaticKeyGeneration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGrpcService<UserServiceImpl>();

await app.RunAsync();
