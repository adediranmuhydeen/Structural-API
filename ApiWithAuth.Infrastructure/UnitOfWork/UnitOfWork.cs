using ApiWithAuth.Core.IRepository;
using ApiWithAuth.Infrastructure.Data;
using ApiWithAuth.Infrastructure.Repository;

namespace ApiWithAuth.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _context;
        public bool Disposable;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEmployeeRepository employeeRepository => _employeeRepository ??= new EmployeeRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChnages()
        {
            await _context.SaveChangesAsync();
        }
    }
}
