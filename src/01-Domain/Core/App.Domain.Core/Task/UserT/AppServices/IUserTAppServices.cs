using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace App.Domain.Core.Task.UserT.AppServices
{
    public interface IUserTAppServices
    {
        public Task<IdentityResult> Register(UserTa model, CancellationToken cToken);
        public Task<IdentityResult> Login(string username, string password, CancellationToken cToken);
    }
}
