using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Data.Repository;

namespace WEB_3_Accountant.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountingContext _context;

        public IPaymentRepository Payment { get; set; }
        public IWeeklyCostRepository WeeklyCost { get; set; }
        public IEmployeeRepository Employee { get; set; }
        public ITaskRepository Task { get; set; }
        public IWeekRepository Week { get; set; }
        public IEmployeeTaskRepository EmployeeTask { get; set; }
        public IProjectRepository Project { get; set; }
        public IHomeRepository Home { get; set; }

        public UnitOfWork()
        {
            _context = new AccountingContext();
            Project = new ProjectRepository(_context);
            EmployeeTask = new EmployeeTaskRepository(_context);
            Payment = new PaymentRepository(_context);
            WeeklyCost = new WeeklyCostRepository(_context);
            Employee = new EmployeeRepository(_context);
            Project = new ProjectRepository(_context);
            Task = new TaskRepository(_context);
            Week = new WeekRepository(_context);
            EmployeeTask = new EmployeeTaskRepository(_context);
            Home = new HomeRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
