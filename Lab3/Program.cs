using Application;
using ApplicationLab4;
using Infrastructure.DbWorkers;
using Infrastructure.Repository;
using InfrastructureLab4.Repositories;
using InfrastructureLab4.Repositories.Interfaces;
using Lab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICourceApiService, CourceApiService>();

//Lab 3
//builder.Services.AddScoped<ICourceService, CourceService>();
//builder.Services.AddScoped<ICourceRepository, CourceRepository>();
//builder.Services.AddScoped<IDbConnection, MSSQLConnection>(serviceProvider => new MSSQLConnection(builder.Configuration.GetConnectionString("DbConnection3")));

//Lab4
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICourceService, CourceServiceLab4>();
builder.Services.AddDbContext<BaseDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection4")));

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
