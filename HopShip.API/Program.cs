using HopShip.Library.Database.Context;
using HopShip.Library.BackgroundService;
using HopShip.Service.ServicesCollection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using HopShip.Repository.ServicesCollection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configurations = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
var connection = configurations["Develop:DataBase:ConnectionString"];
builder.Services.AddDbContext<ContextForDb>(options => options.UseNpgsql(connection), ServiceLifetime.Scoped);

// Mappatura dell'interfaccia IContextForDb<ContextForDb> al DbContext concreto
builder.Services.AddScoped<IContextForDb<ContextForDb>>(provider => provider.GetRequiredService<ContextForDb>());

builder.Services.AddSharedRepositoryServices();
builder.Services.AddSharedServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBackgroundWorkers(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
