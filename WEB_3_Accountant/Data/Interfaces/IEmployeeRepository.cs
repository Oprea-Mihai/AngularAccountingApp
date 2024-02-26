using WEB_3_Accountant.Data.Entities;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        public Employee GetByName(string name);
        public bool checkExistingEmployee(string name);
    }
}
