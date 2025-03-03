using HopShip.DatabaseService.Context;
using HopShip.Worker.Database;
using HopShip.Worker.Database.ServicesCollection;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSharedServices();

var configurations = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
var connection = configurations["Develop:DataBase:ConnectionString"];

builder.Services.AddDbContext<ContextForDb>(options => options.UseNpgsql(connection), ServiceLifetime.Scoped);
builder.Services.AddScoped<IContextForDb<ContextForDb>, ContextForDb>();

var host = builder.Build();
host.Run();
