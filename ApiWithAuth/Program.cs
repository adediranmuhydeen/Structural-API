using ApiWithAuth.Core.Utilities;
using ApiWithAuth.Extensions;
using ApiWithAuth.Infrastructure;
using ApiWithAuth.Infrastructure.Data;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(x =>
                {
                    x.Authority = "https://localhost:7180/"; //idp address
                    x.RequireHttpsMetadata = false;
                    x.ApiName = "ApiWithAuth"; //api name
                });

builder.Services.AddSingleton(mapper);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.UseExceptionHandler("/globalexceptionhnadler");

app.Run();
