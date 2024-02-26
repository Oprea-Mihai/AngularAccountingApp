using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Repository;
using WEB_3_Accountant.Data.Interfaces;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IPaymentRepository Payment { get; set; }
        IEmployeeRepository Employee { get; set; }
        IWeeklyCostRepository WeeklyCost { get; set; }
        ITaskRepository Task { get; set; }
        IWeekRepository Week { get; set; }  
        IEmployeeTaskRepository EmployeeTask { get; set; }
        IProjectRepository Project { get; set; }
        IHomeRepository Home { get; set; }

        int Complete();

    }
}
