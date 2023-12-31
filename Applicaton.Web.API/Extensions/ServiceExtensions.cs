using System.Reflection;
using System.Text;
using System.Text.Json;
using Application.Web.Database.Constants;
using Application.Web.Database.Context;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Queries.ServiceQueries;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Applicaton.Web.API.Extensions
{
	public static class ServiceExtensions
	{

		// Database Connection
		public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationContext>(option =>
				option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
				b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)),
				ServiceLifetime.Transient);
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				var startTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")).ToString();

				var swaggerDescription = "## Description. \n\n" +
				"- This is a list of APIs we use to manage the MotorMate Application. Please note that the MotorMate Application relies on these APIs to deliver a seamless and feature-rich experience to our users. \n\n" +
				"\n\n" +
				$"* Last updated at: __{startTime}__ \n\n";

				OpenApiInfo apiInfo = new OpenApiInfo
				{
					Title = "MotorMate Swagger UI",
					Description = swaggerDescription,
					Version = "develop"
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
		}

		public static void ConfigureAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile).Assembly);
		}

		public static void ConfigureSignalR(this IServiceCollection services)
		{
			services.AddSignalR();
		}

		public static void ConfigureController(this IServiceCollection services)
		{
			services.AddControllers();
		}

		public static void ConfigureCache(this IServiceCollection services)
		{
			services.AddLazyCache();
			services.AddSingleton<CacheKeyConstants>();
		}

		public static void ConfigureIdentity(this IServiceCollection services)
		{
			var builder = services.AddIdentity<User, Role>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
			})
			.AddEntityFrameworkStores<ApplicationContext>()
			.AddDefaultTokenProviders();
		}

		public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtConfig = configuration.GetSection("JwtConfig");
			var googleConfig = configuration.GetSection("Google");
			var secretKey = jwtConfig["serect"];
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddGoogle(options =>
			{
				options.ClientId = googleConfig["ClientId"];
				options.ClientSecret = googleConfig["ClientSecret"];
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtConfig["validIssuer"],
					ValidAudience = jwtConfig["validAudience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
				};

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Query["access_token"];
						var path = context.HttpContext.Request.Path;

						if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/messages")))
						{
							context.Token = accessToken;
						}

						return Task.CompletedTask;
					}
				};
			});
		}

		public static void RegisterServiceDependencies(this IServiceCollection services)
		{
			services.AddScoped<IWeatherForcastService, WeatherForcastService>();
			services.AddScoped<IUtilitiesService, UtilitiesService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IBrandService, BrandService>();
			services.AddScoped<IModelService, ModelService>();
			services.AddScoped<ICollectionService, CollectionService>();
			services.AddScoped<IColorService, ColorService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IVehicleService, VehicleService>();
			services.AddScoped<IChatService, ChatService>();
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<IBlogCategoryService, BlogCategoryService>();
			services.AddScoped<IBlogService, BlogService>();
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<ICheckoutService, CheckoutService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IMockDataService, MockDataService>();
			services.AddScoped<IBlogCommentService, BlogCommentService>();
			services.AddScoped<IChartService, ChartService>();
		}

		public static void RegisterQueryDependencies(this IServiceCollection services)
		{
			services.AddScoped<IBrandQueries, BrandQueries>();
			services.AddScoped<IModelQueries, ModelQueries>();
			services.AddScoped<ICollectionQueries, CollectionQueries>();
			services.AddScoped<IColorQueries, ColorQueries>();
			services.AddScoped<IVehicleQueries, VehicleQueries>();
			services.AddScoped<IChatQueries, ChatQueries>();
			services.AddScoped<IMessageQueries, MessageQueries>();
			services.AddScoped<ICartQueries, CartQueries>();
			services.AddScoped<IUserQueries, UserQueries>();
			services.AddScoped<IBlogQueries, BlogQueries>();
			services.AddScoped<IBlogCategoryQueries, BlogCategoryQueries>();
			services.AddScoped<ICheckoutOrderQueries, CheckoutOrderQueries>();
			services.AddScoped<ITripRequestQueries, TripRequestQueries>();
			services.AddScoped<IVehicleReviewQueries, VehicleReviewQueries>();
			services.AddScoped<IBlogCommentQueries, BlogCommentQueries>();
		}

		public static void RegistryDatabaseDependencies(this IServiceCollection services)
		{
			services.AddScoped<IApplicationContext, ApplicationContext>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					string[] exposedHeaders = { "X-Pagination", "content-Type" };
					string[] allowOrigins = {
												"https://motormate.vercel.app",
												"http://motormate.vercel.app",
												"http://localhost:3000",
												"https://motormate-admin.vercel.app",
												"http://motormate-admin.vercel.app",
												"https://motormate-frontend-web-client.azurewebsites.net",
												"https://motormate-frontend-web-admin.azurewebsites.net",
												"http://motormate-frontend-web-client.azurewebsites.net",
												"http://motormate-frontend-web-admin.azurewebsites.net"
											};


					policy.WithExposedHeaders(exposedHeaders)
						  .SetIsOriginAllowed(_ => true)
						  .WithOrigins(allowOrigins)
						  .AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowCredentials();
				});
			});
		}

		public static void AuthorizedPolicies(this IServiceCollection services)
		{
			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminRight", policy =>
						policy.RequireRole(
								SeedDatabaseConstant.ADMIN.Name, 
								SeedDatabaseConstant.STAFF.Name));

				options.AddPolicy("UserRight", policy =>
						policy.RequireRole(
								SeedDatabaseConstant.ADMIN.Name,
								SeedDatabaseConstant.STAFF.Name,
								SeedDatabaseConstant.LESSOR.Name,
								SeedDatabaseConstant.USER.Name
							));
			});
		}

		public static void AddPaginationHeader(this HttpResponse response, PaginationMetadata pagination)
		{
			var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

			response.Headers.Add("Access-Control-Allow-Origin", "*"); //T add expose headers xong bi block CORS (local) nen tam thoi t add tam. nhu vay

			response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination, options));

			response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
		}


	}
}
