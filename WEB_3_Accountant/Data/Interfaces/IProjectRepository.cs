using WEB_3_Accountant.Data.Entities;
using Task = WEB_3_Accountant.Data.Entities.Task;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        public IEnumerable<Project> GetUnfinishedProjects();
        public void SetProjectFinished(string name);
        public Project GetByName(string name);
        public ICollection<Task> GetAssignedTasks(string name);
    }
}
