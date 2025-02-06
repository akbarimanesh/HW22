using App.Domain.Core.Entities;
using App.Domain.Core.Task.Configs;
using App.Domain.Core.Task.TaskUser.AppServices;
using App.Domain.Core.Task.TaskUser.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Task.TaskUser
{
    public class TaskUserAppServices: ITaskUserAppServices
    {
        private readonly ITaskUserServices _TaskUserServices;
        private readonly SiteSettings _SiteSettings;
        public TaskUserAppServices(ITaskUserServices taskUserServices, SiteSettings siteSettings)
        {
            _TaskUserServices = taskUserServices;
            _SiteSettings = siteSettings;

        }

        public async Task<Result> CreateT(TaskU taskUser, CancellationToken cToken)
        {
            if(await _TaskUserServices.NumberTask(taskUser.UserTaId, cToken)>= _SiteSettings.NumberUnfinishedTasks)
            {
                return new Result(false, " تعداد وظایف ناتمام شما بیش از مقدار تعیین شده است..");
            }
            else
            {
                await _TaskUserServices.CreateTask(taskUser, cToken);
                return new Result(true, "با موفقیت اضافه شد.");
            }
          
        }

        public async Task<Result> DeleteT(int id, CancellationToken cToken)
        {
            if (await _TaskUserServices.GetTaskById(id, cToken) != null)
            {
                await _TaskUserServices.DeleteTask(id, cToken);
                return new Result(true, "با موفقیت حذف شد.");
            }
            else
                return new Result(false, " همچین وظیفه ای وجود ندارد.");
        }

        public async Task<Result> EndTask(int id, CancellationToken cToken)
        {
            if (await _TaskUserServices.GetTaskById(id, cToken) != null)
            {
                await _TaskUserServices.EndTask(id, cToken);
                return new Result(true, "با موفقیت به پایان رسید.");
            }
            else
                return new Result(false, "همچین وظیفه ای وجود ندارد.");
        }

        public async Task<List<GetTaskDto>> GetAllT(int id, CancellationToken cToken)
        {
            return await _TaskUserServices.GetAllTask(id, cToken);
        }

        public async Task<TaskU> GetT(int id, CancellationToken cToken)
        {
            if (await _TaskUserServices.GetTaskById(id,cToken) != null)
            {
                return await _TaskUserServices.GetTaskById(id,cToken);

            }
            else
                return null;
        }

        public async Task<int> NumberTask(int id, CancellationToken cToken)
        {
           return await _TaskUserServices.NumberTask(id, cToken);
        }

        public async Task<Result> UpdateT(TaskU taskUser, CancellationToken cToken)
        {
            
            if (await _TaskUserServices.GetTaskById(taskUser.Id, cToken) != null)
            {
                await _TaskUserServices.UpdateTask(taskUser, cToken);
                return new Result(true, "با موفقیت ویرایش شد.");
            }
            else
                return new Result(false, "همچین وظیفه ای وجود ندارد.");
        }
    }
}
