using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IHomeRepository : IGenericRepository<Week>
    {
        public List<WeekInfoModel> GetWeekInfo();

    }

}
