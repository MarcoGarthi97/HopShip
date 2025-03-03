using HopShip.Worker.Database;
using HopShip.Worker.Database.Context;
using HopShip.Worker.Database.ServicesCollection;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSharedServices();

var connection = builder.Configuration["Develop:Database:ConnectionStrings"];

builder.Services.AddDbContext<HopDbContext>(options => options.UseNpgsql(connection));
 
var host = builder.Build();
host.Run();
