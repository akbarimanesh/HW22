
using App.Domain.Core.Entities;
using App.Domain.Core.Task.TaskUser.Data;
using App.Domain.Core.Task.TaskUser.Services;



namespace App.Domain.Services.Task.TaskUser
{
    public class TaskUserServices: ITaskUserServices
    {
        private readonly ITaskUserRepository _TaskUserRepository;

        public TaskUserServices(ITaskUserRepository taskUserRepository)
        {
            _TaskUserRepository = taskUserRepository;
        }

        public async System.Threading.Tasks.Task CreateTask(TaskU taskUser, CancellationToken cToken)
        {
            await _TaskUserRepository.CreateTask(taskUser, cToken);
        }

        public async System.Threading.Tasks.Task DeleteTask(int id, CancellationToken cToken)
        {
            await _TaskUserRepository.DeleteTask(id, cToken);
        }

        public async System.Threading.Tasks.Task EndTask(int id, CancellationToken cToken)
        {
            await _TaskUserRepository.EndTask(id, cToken);
        }

        public async Task<List<GetTaskDto>> GetAllTask(int id, CancellationToken cToken)
        {
            return await _TaskUserRepository.GetAllTask(id, cToken);
        }

        public async Task<TaskU> GetTaskById(int id, CancellationToken cToken)
        {
            return await _TaskUserRepository.GetTaskById(id, cToken);
        }

        public async Task<int> NumberTask(int id, CancellationToken cToken)
        {
            return await _TaskUserRepository.NumberTask(id, cToken);
        }

        public async System.Threading.Tasks.Task UpdateTask(TaskU taskUser, CancellationToken cToken)
        {
            await _TaskUserRepository.UpdateTask(taskUser, cToken);
        }
    }
}
