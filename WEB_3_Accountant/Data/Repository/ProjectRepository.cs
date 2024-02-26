using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using Task = WEB_3_Accountant.Data.Entities.Task;

namespace WEB_3_Accountant.Data.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AccountingContext context) : base(context)
        {
        }
        public IEnumerable<Project>GetUnfinishedProjects()
        {
            return _context.Projects.Where(p => p.IsFinished == false);
        }

        public void SetProjectFinished(string name)
        {
            _context.Projects.Where(p=>p.Name==name).FirstOrDefault().IsFinished=true;
        }

        public Project GetByName(string name)
        {
            return _context.Projects.Where(x => x.Name == name).FirstOrDefault();
        }

        public ICollection<Task> GetAssignedTasks(string name)
        {
            return _context.Projects.Where(x => x.Name == name).FirstOrDefault().Tasks;
        }
    }
}
