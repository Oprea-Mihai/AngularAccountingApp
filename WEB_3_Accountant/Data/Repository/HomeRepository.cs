using System.Linq.Expressions;
using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.Data.Repository
{
    public class HomeRepository : GenericRepository<Week>, IHomeRepository
    {
        public HomeRepository(AccountingContext context) : base(context)
        {
        }

        public List<WeekInfoModel> GetWeekInfo()
        {
            var weeks = new List<WeekInfoModel>();
            foreach (Week w in this.GetAll().OrderBy(o => o.StartDate))
            {
                var week = new WeekInfoModel();
                week.TotalGrossAmount = (double)w.TotalGrossAmount;
                week.TotalNetAmount = (double)w.TotalNetAmount;
                week.Date = w.StartDate.ToString("MM.dd.yy") + " - " +w.EndDate.ToString("MM.dd.yy");
                weeks.Add(week);
            }
            return weeks;
        }

    }
}