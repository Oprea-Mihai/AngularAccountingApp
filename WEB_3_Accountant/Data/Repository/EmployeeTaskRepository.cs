using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.Data.Repository
{
    public class EmployeeTaskRepository : GenericRepository<EmployeeTask>, IEmployeeTaskRepository
    {
        public EmployeeTaskRepository(AccountingContext context) : base(context)
        {
        }

        public IQueryable<EmployeeTask> GetAllByEmployeeId(int employeeId)
        {
            return _context.EmployeeTasks.Where(x => x.EmployeeId == employeeId);
        }

        public IEnumerable<EmployeeTask> GetAllByEmployeeIdAndWeeklyCostId(int employeeId, int weeklycostId)
        {
            return _context.EmployeeTasks
                .Where(item => item.EmployeeId == employeeId
                && item.WeeklyCostId == weeklycostId);
        }

        public EmployeeTask GetEmployeeTaskByEmployeeAndWeeklyCostIdAndTaskId(int employeeId, int weeklycostId, int taskId)
        {
            return _context.EmployeeTasks
                .Where(item => item.EmployeeId == employeeId 
                && item.WeeklyCostId == weeklycostId
                && item.TaskId == taskId).FirstOrDefault();
        }

        public IEnumerable<EmployeeTask> GetEmployeeTaskWithoutSpecialDuplicated()
        {
            List<EmployeeTask> employeeTasksFinal = new List<EmployeeTask>();
            var employeeTasks = _context.EmployeeTasks.Where(x => x.WorkedHours != "0").ToList();
            var employeeSpecialTasks = _context.EmployeeTasks.Where(x => x.WorkedHours == "0").ToList();

            employeeTasksFinal = employeeTasks;

            foreach(var employeeTask in employeeSpecialTasks)
            {
                if(employeeTasksFinal.Where(stask => stask.TaskId == employeeTask.TaskId && stask.EmployeeId == employeeTask.EmployeeId && stask.WeeklyCostId == employeeTask.WeeklyCostId).FirstOrDefault() == null)
                {
                    employeeTasksFinal.Add(employeeTask);
                }
            }

            return employeeTasksFinal;
        }
    }

}
