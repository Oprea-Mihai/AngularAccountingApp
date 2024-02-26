using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;

namespace WEB_3_Accountant.Data.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AccountingContext context) : base(context)
        {
           
        }

        public Employee GetByName(string name)
        {
            return _context.Employees.Where(employee => employee.Name == name).FirstOrDefault();
        }

        public bool checkExistingEmployee(string name)
        {
            if (_context.Employees.Where(employee => employee.Name == name).FirstOrDefault()!=null)
                return true;
            return false;
        }
    }
}
