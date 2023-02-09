using ApiWithAuth.Core.Utilities;
using ApiWithAuth.Extensions;
using ApiWithAuth.Infrastructure;
using ApiWithAuth.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));
builder.Services.Register();

var automapper = new MapperConfiguration(x => x.AddProfile(new MapInitializer()));
IMapper mapper = automapper.CreateMapper();

//Extention methods
builder.Services.ScopedInjection();
builder.Services.SwaggerHandler();
builder.Services.IdentitySethings();
builder.Services.AuthSettings(configuration);
builder.Services.AddSingleton(mapper);

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
    //.MinimumLevel.Error().WriteTo
    //.File("C:\\Users\\Decagon\\OneDrive\\Desktop\\Structural-API\\ApiWithAuth\\ErrorLogger.log",
    //rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler();
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
