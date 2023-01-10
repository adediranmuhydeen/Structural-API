using ApiWithAuth.Core.Domain;
using ApiWithAuth.Core.IRepository;
using ApiWithAuth.Infrastructure.Data;

namespace ApiWithAuth.Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
