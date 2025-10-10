using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SwimCheck.API.Data;
using SwimCheck.API.Mappings;
using SwimCheck.API.Repositories.Interfaces;
using SwimCheck.API.Repositories.Repos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add Swagger with JWT Authentication
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SwimCheck API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
            },
            new List<string>()
        },
    });
});




//EF Core + SQL Server
builder.Services.AddDbContext<SwimCheckDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SwimCheckConnectionString")));

builder.Services.AddDbContext<SwimCheckAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SwimCheckAuthConnectionString")));

// Repositories
builder.Services.AddScoped<IAthleteRepository, SQLAthleteRepository>();
builder.Services.AddScoped<IRaceRepository, SQLRaceRepository>();
builder.Services.AddScoped<IEnrollRepository, SQLEnrollRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Identity simple user (packages)
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("SwimCheck")
    .AddEntityFrameworkStores<SwimCheckAuthDbContext>()
    .AddDefaultTokenProviders();

// Password configs
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


//Key = name of the property with error, Value = list of error messages | generic response

//builder.Services.AddControllers()
//    .ConfigureApiBehaviorOptions(options =>
//    {
//        options.InvalidModelStateResponseFactory = context =>
//        {
//            var erros = context.ModelState
//                .Where(e => e.Value!.Errors.Count > 0)
//                .ToDictionary(
//                    e => e.Key,
//                    e => e.Value!.Errors.Select(er => er.ErrorMessage)); 
//            var payload = new
//            {
//                Mensagem = "Os dados enviados são inválidos",
//                Erros = erros
//            };

//            return new BadRequestObjectResult(payload);
//        };
//    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
