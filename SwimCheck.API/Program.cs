using Microsoft.EntityFrameworkCore;
using SwimCheck.API.Data;
using SwimCheck.API.Mappings;
using SwimCheck.API.Repositories.Interfaces;
using SwimCheck.API.Repositories.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EF Core + SQL Server
builder.Services.AddDbContext<SwimCheckDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Repositories
builder.Services.AddScoped<IAthleteRepository, SQLAthleteRepository>();
builder.Services.AddScoped<IRaceRepository, SQLRaceRepository>();


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
