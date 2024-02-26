using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using System.Linq.Expressions;

namespace WEB_3_Accountant.Data.Repository
{
    public class WeeklyCostRepository : GenericRepository<WeeklyCost>, IWeeklyCostRepository
    {
        public WeeklyCostRepository(AccountingContext context) : base(context)
        {
        }

        public IEnumerable<WeeklyCost> GetAllById(int weekId)
        {
            return _context.WeeklyCosts.Where(c => c.WeekId == weekId);
        }

        public WeeklyCost GetByEmployeeIdAndWeekId(int employeeId, int weekId)
        {
            return _context.WeeklyCosts.Where(item => item.EmployeeId == employeeId && item.WeekId == weekId).FirstOrDefault();
        }
    }
}
