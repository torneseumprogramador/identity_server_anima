using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Identity.Domain.Entities;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Repositories.Interfaces;
using Identity.Domain.Services;
using Identity.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<AppContext>(options =>
    options.UseMySql(configuration.GetConnectionString("cnn"), new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<ICrypto, Crypto>();
builder.Services.AddScoped<ITokenJwt, TokenJwt>();
builder.Services.AddScoped<IRepository<Administrator>, Repository<Administrator>>();

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
