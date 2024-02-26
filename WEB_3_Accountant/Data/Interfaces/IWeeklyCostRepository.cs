using WEB_3_Accountant.Data.Entities;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IWeeklyCostRepository : IGenericRepository<WeeklyCost>
    {
        public IEnumerable<WeeklyCost> GetAllById(int weekId);
        public WeeklyCost GetByEmployeeIdAndWeekId(int employeeId,int weekId);
    }
}
