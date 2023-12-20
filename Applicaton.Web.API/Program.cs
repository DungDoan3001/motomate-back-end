using Applicaton.Web.API.Extensions;
using Applicaton.Web.API.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegistryDatabaseDependencies();
builder.Services.RegisterServiceDependencies();
builder.Services.RegisterQueryDependencies();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureController();
builder.Services.AuthorizedPolicies();
builder.Services.ConfigureSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

app.UseSwagger();

app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "MotorMate v1");
});


app.UseCors("CorsPolicy");

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("messages");

app.MapHub<MessageHub>("chat-details");

app.Run();
