using Task = WEB_3_Accountant.Data.Entities.Task;

namespace WEB_3_Accountant.Data.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Entities.Task>
    {
        public Task GetTaskByName(string name);
        public decimal GetSpecialTaskValueByName(string name);
        public IEnumerable<Task> GetTaskByProjectID(int projectId);
    }
}
