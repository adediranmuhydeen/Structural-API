using ApiWithAuth.Core.IRepository;
using ApiWithAuth.Core.IService;
using ApiWithAuth.Infrastructure.Repository;
using ApiWithAuth.Services.EmployeeS;
using Microsoft.Extensions.DependencyInjection;

namespace ApiWithAuth.Infrastructure
{
    public static class DIRegister
    {
        public static void Register(this IServiceCollection myservice)
        {
            myservice.AddScoped<IEmployeeRepository, EmployeeRepository>();
            myservice.AddScoped<IEmployeeService, EmployeeService>();
            myservice.AddScoped<IUserService, UserService>();
            myservice.AddTransient<IMailService, MailService>();
        }
    }
}
