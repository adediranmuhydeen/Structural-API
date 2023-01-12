using ApiWithAuth.Core.IRepository;
using ApiWithAuth.Core.IService;
using ApiWithAuth.Core.Utilities;
using ApiWithAuth.Infrastructure.Data;
using ApiWithAuth.Infrastructure.Repository;
using ApiWithAuth.Infrastructure.UnitOfWork;
using ApiWithAuth.Services.EmployeeS;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

var automapper = new MapperConfiguration(x => x.AddProfile(new MapInitializer()));
IMapper mapper = automapper.CreateMapper();

builder.Services.AddSingleton(mapper);

//Add Servicess
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
