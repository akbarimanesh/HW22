using App.Domain.Core.Entities;
using App.Domain.Core.Task.UserT.AppServices;
using App.EndPoints.Mvc.Task.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.Mvc.Task.Controllers
{
    public class UserTController : Controller
    {
        private readonly IUserTAppServices _UserTAppServices ;
        private readonly SignInManager<UserTa> _signInManager;

        public UserTController(IUserTAppServices userTAppServices, SignInManager<UserTa> signInManager)
        {
            _UserTAppServices = userTAppServices;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Register(CancellationToken cToken)
        {

           
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
                return View(user);
            if (string.IsNullOrWhiteSpace(user.Password)|| string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Email))
            {
                ModelState.AddModelError(string.Empty, "فیلدهای خالی رو پر کنید.");
                return View(user);
            }
            var user1 = new UserTa { UserName = user.Username, Email = user.Email ,PasswordHash=user.Password};
            var result = await _UserTAppServices.Register(user1, cToken);
            
            if (result.Succeeded)
            {
               
                return RedirectToAction("Login", "UserT");
            }

            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }
           

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(UserTa user, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
                return View(user);
            if (string.IsNullOrWhiteSpace(user.PasswordHash) || string.IsNullOrWhiteSpace(user.UserName) )
            {
                ModelState.AddModelError(string.Empty, "فیلدهای خالی رو پر کنید.");
                return View(user);
            }
            var result = await _UserTAppServices.Login(user.UserName, user.PasswordHash, cToken);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            else
            {
                ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است.");
            }

            return View();
        }
           

       
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cToken)
        {


            return View();


        }

       
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
   
    

