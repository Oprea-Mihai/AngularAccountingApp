using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;

namespace WEB_3_Accountant.Data.Repository
{
    public class WeekRepository : GenericRepository<Week>, IWeekRepository
    {
        public WeekRepository(AccountingContext context) : base(context)
        {
        }

        public Week GetWeekByDate(DateTime startDate, DateTime endDate)
        {
            return _context.Weeks.Where(item => item.StartDate.Date == startDate.Date && item.EndDate.Date == endDate.Date).FirstOrDefault();
        }
    }
}
