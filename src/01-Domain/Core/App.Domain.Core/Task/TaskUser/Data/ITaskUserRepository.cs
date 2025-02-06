
using App.Domain.Core.Entities;


namespace App.Domain.Core.Task.TaskUser.Data
{
    public interface ITaskUserRepository
    {
        
        public global::System.Threading.Tasks.Task CreateTask(TaskU taskUser, CancellationToken cToken);
        public Task<List<GetTaskDto>> GetAllTask(int id, CancellationToken cToken);
        public global::System.Threading.Tasks.Task UpdateTask(TaskU taskUser, CancellationToken cToken);
        public global::System.Threading.Tasks.Task DeleteTask(int id, CancellationToken cToken);
        public Task<TaskU> GetTaskById(int id, CancellationToken cToken);
        public System.Threading.Tasks.Task EndTask(int id,CancellationToken cToken);
        public Task<int> NumberTask(int id, CancellationToken cToken);
     
    }
}
