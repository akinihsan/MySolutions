using Farm.API;
using Farm.Infrastructure.DBContext;
using Farm.Infrastructure.Repositories.Abstract;
using Farm.Infrastructure.Repositories.Concrete;
using Farm.Service.Abstract;
using Farm.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Serialize enums as strings globally.
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddMvc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Farm.Api", Version = "v1" });

    c.UseAllOfToExtendReferenceSchemas();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
"JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

builder.Services.AddAutoMapper((a) => a.AddProfile<MappingProfile>());
builder.Services.AddDbContext<FarmDbContext>(ServiceLifetime.Scoped);

builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();
builder.Services.AddTransient<IAnimalService, AnimalService>();

var spaBaseUrl = builder.Configuration.GetSection("APISettings:SPABaseUrl").Value;
builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(spaBaseUrl)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));




var jwtAudience = builder.Configuration.GetSection("JWT:ValidAudience").Value;
var jwtValidIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Value;
var jwtSecret = builder.Configuration.GetSection("JWT:Secret").Value;

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidIssuer = jwtValidIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(IdentityConstants.ApplicationScheme);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseCors("AllowSpecificOrigin");


app.UseAuthorization();

app.MapControllers();

app.Run();
