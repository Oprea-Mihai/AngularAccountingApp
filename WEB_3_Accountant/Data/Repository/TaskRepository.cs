using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using Task = WEB_3_Accountant.Data.Entities.Task;

namespace WEB_3_Accountant.Data.Repository
{
    public class TaskRepository : GenericRepository<Entities.Task>, ITaskRepository
    {
        public TaskRepository(Entities.AccountingContext context) : base(context)
        {
        }

        public Task GetTaskByName(string name)
        {                          
                return _context.Tasks.Where(task => task.Name.ToLower() == name.ToLower()).FirstOrDefault();           
        }

        public decimal GetSpecialTaskValueByName(string name)
        {
            try
            {
                var task = _context.Tasks.Where(task => task.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (task == null)
                    throw new Exception();

                return (decimal)_context.Tasks.Where(task => task.Name.ToLower() == name.ToLower()).FirstOrDefault().Income;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IEnumerable<Entities.Task> GetTaskByProjectID(int projectID)
        {
            return _context.Tasks.Where(task => task.ProjectId == projectID);
        }

    }
}
