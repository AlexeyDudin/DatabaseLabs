using Application;
using Infrastructure.DbWorkers;
using Infrastructure.Repository;
using Lab3;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICourceApiService, CourceApiService>();
builder.Services.AddScoped<ICourceService, CourceService>();
builder.Services.AddScoped<ICourceRepository, CourceRepository>();
builder.Services.AddScoped<IDbConnection, MSSQLConnection>(serviceProvider => new MSSQLConnection(builder.Configuration.GetConnectionString("DbConnection")));

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
