

using App.Domain.Core.Entities;
using App.Domain.Core.Task.TaskUser.Data;

using App.Infra.Data.Db.SqlServer.Ef.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Task.TaskUser
{
    public class TaskUserRepository : ITaskUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public TaskUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async System.Threading.Tasks.Task CreateTask(TaskU taskUser, CancellationToken cToken)
        {
            
           await _appDbContext.TaskUs.AddAsync(taskUser);
           await _appDbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteTask(int id, CancellationToken cToken)
        {
            var model = await _appDbContext.TaskUs.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.TaskUs.Remove(model);
            await _appDbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task EndTask(int id, CancellationToken cToken)
        {
            var model = await _appDbContext.TaskUs.FirstOrDefaultAsync(x => x.Id == id);
            model.IsCompleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<GetTaskDto>> GetAllTask(int id, CancellationToken cToken)
        {
            return await _appDbContext.TaskUs.AsNoTracking().Where(x => x.UserTaId == id)
                .Select(x => new GetTaskDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsCompleted = x.IsCompleted,
                    
                }).ToListAsync();
        }


        public async Task<TaskU> GetTaskById(int id, CancellationToken cToken)
        {
            return await _appDbContext.TaskUs.AsNoTracking().Where(x=>x.Id== id).FirstOrDefaultAsync();
           
        }

        public async Task<int> NumberTask(int id, CancellationToken cToken)
        {
            var x= await _appDbContext.TaskUs.AsNoTracking().Where(x => x.UserTaId == id && x.IsCompleted==false).CountAsync();
            return x;
        }

        public async System.Threading.Tasks.Task UpdateTask(TaskU taskUser, CancellationToken cToken)
        {
            var taskU = await _appDbContext.TaskUs.FirstOrDefaultAsync(p => p.Id == taskUser.Id);
            taskU.Title = taskUser.Title;
            taskU.Description = taskUser.Description;
            taskU.IsCompleted= taskUser.IsCompleted;
           
            await _appDbContext.SaveChangesAsync();
        }
    }
}
