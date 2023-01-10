namespace ApiWithAuth.Core.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository employeeRepository { get; }
        Task SaveChnages();
    }
}
