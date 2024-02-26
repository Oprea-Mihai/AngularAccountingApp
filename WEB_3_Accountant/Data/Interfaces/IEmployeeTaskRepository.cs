using WEB_3_Accountant.Data.Entities;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IEmployeeTaskRepository : IGenericRepository<EmployeeTask>
    {

        public EmployeeTask GetEmployeeTaskByEmployeeAndWeeklyCostIdAndTaskId(int employeeId, int weeklycostId,int taskId);
        public IQueryable<EmployeeTask> GetAllByEmployeeId(int employeeId);
        public IEnumerable<EmployeeTask> GetAllByEmployeeIdAndWeeklyCostId(int employeeId,int weeklycostId);
        public IEnumerable<EmployeeTask> GetEmployeeTaskWithoutSpecialDuplicated();

    }
    
}
