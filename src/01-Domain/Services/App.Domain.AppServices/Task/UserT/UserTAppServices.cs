using App.Domain.Core.Entities;
using App.Domain.Core.Task.UserT.AppServices;

using Microsoft.AspNetCore.Identity;


namespace App.Domain.AppServices.Task.UserT
{
    public class UserTAppServices : IUserTAppServices
    {

        private readonly UserManager<UserTa> _userManager;
        private readonly SignInManager<UserTa> _signInManager;
        public UserTAppServices(UserManager<UserTa> userManager, SignInManager<UserTa> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;

        }
        public async Task<IdentityResult> Login(string username, string password, CancellationToken cToken)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();
        }        

        public async Task<IdentityResult> Register(UserTa model, CancellationToken cToken)
        {
            var user = new UserTa
            {
                UserName = model.UserName,
                Email = model.Email,
               PasswordHash = model.PasswordHash

            };


            var result = await _userManager.CreateAsync(user, model.PasswordHash);

            if (result.Succeeded)
            {
                 await _userManager.AddToRoleAsync(user, "Admin");
            }
           
            return result;
        }
    }
}
