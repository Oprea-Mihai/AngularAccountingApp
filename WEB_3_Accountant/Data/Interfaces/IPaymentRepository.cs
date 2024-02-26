using WEB_3_Accountant.Data.Entities;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Week>
    {
        public Week GetUnpaidWeek();
        public IQueryable<Week> GetPaidWeeks();
        public Week GetByDate(DateTime startDate);
        public Week GetByDateAndPaid(DateTime startDate);
    }
}
