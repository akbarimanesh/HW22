

using App.Domain.Core.Entities;

namespace App.Domain.Core.Task.TaskUser.AppServices
{
    public interface ITaskUserAppServices
    {
        public Task<TaskU> GetT(int id, CancellationToken cToken);
        public Task<Result> CreateT(TaskU taskUser, CancellationToken cToken);
        public Task<List<GetTaskDto>> GetAllT(int id, CancellationToken cToken);
        public Task<Result> UpdateT(TaskU taskUser, CancellationToken cToken);
        public Task<Result> DeleteT(int id, CancellationToken cToken);
        public Task<Result> EndTask(int id, CancellationToken cToken);
        public Task<int> NumberTask(int id, CancellationToken cToken);
    } 
}
