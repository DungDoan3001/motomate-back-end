using System.Reflection;
using Application.Web.Database.Context;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using Applicaton.Web.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
services.AddControllers();

// Database connection
services.AddDbContext<ApplicationContext>(option => 
                option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    OpenApiInfo apiInfo = new OpenApiInfo
    {
        Title = "MotoMate Swagger UI",
        Description = "This is a list of APIs we use to manage the MotoMate Application",
    };
    options.SwaggerDoc("v1", apiInfo);

    // Generating api description via xml;
    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    // Add authentication button
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter your token here. The token is in JWT format",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});

// Auto Mapper
services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Dependency Injection
services.AddScoped<IApplicationContext, ApplicationContext>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IWeatherForcastService, WeatherForcastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    
//}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MotoMate v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
