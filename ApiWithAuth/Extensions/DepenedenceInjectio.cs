using ApiWithAuth.Core.IRepository;
using ApiWithAuth.Core.IService;
using ApiWithAuth.Infrastructure.Repository;
using ApiWithAuth.Infrastructure.UnitOfWork;
using ApiWithAuth.Services.EmployeeS;

namespace ApiWithAuth.Extensions
{
    public static class DepenedenceInjectio
    {
        public static void ScopedInjection(this IServiceCollection services)
        {
            //Add Servicess
            services.AddAutoMapper(typeof(Program));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
        }
    }
}
