using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using System.Linq.Expressions;

namespace WEB_3_Accountant.Data.Repository
{
    public class PaymentRepository : GenericRepository<Week>, IPaymentRepository
    {
        public PaymentRepository(AccountingContext context) : base(context)
        {
        }

        public Week? GetByDate(DateTime startDate)
        {
            return _context.Weeks.Where(x => x.StartDate == startDate).FirstOrDefault();
        }

        public Week? GetByDateAndPaid(DateTime startDate)
        {
            return _context.Weeks.Where(x => x.StartDate == startDate && x.IsPaid == true).FirstOrDefault();
        }

        public IQueryable<Week> GetPaidWeeks()
        {
            return _context.Weeks.Where(x => x.IsPaid == true);
        }

        public Week GetUnpaidWeek()
        {
            return _context.Weeks.Where(x => x.IsPaid == false).FirstOrDefault();
        }
    }
}
