using WEB_3_Accountant.Data.Entities;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IWeekRepository : IGenericRepository<Week>
    {
        public Week GetWeekByDate(DateTime startDate, DateTime endDate);
    }
    
}
