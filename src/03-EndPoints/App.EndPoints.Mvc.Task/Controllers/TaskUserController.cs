using App.Domain.Core.Entities;
using App.Domain.Core.Task.TaskUser.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.Mvc.Task.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TaskUserController : Controller
    {
        private readonly ITaskUserAppServices _TaskUserAppServices;
        private readonly UserManager<UserTa> _userManager;

      
        public TaskUserController(ITaskUserAppServices taskUserAppServices, UserManager<UserTa> userManager)
        {
            _TaskUserAppServices = taskUserAppServices;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(CancellationToken cToken)
        {
            var userId = _userManager.GetUserId(User);
            var id = int.Parse(userId);
            var number = await _TaskUserAppServices.NumberTask(id, cToken);

            ViewBag.TaskCount = number;
            var tasks = await _TaskUserAppServices.GetAllT( id, cToken);
           
            return View(tasks);

            
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskU model, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                model.UserTaId = int.Parse(userId);
                var result = await _TaskUserAppServices.CreateT(model, cToken);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.IsMessage;
                   

                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;
                   
                }
                return RedirectToAction(nameof(Index));
            }
               
            
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Preview(int id, CancellationToken cToken)
        {

            var model = await _TaskUserAppServices.GetT(id, cToken);



            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cToken)
        {
            var userId = _userManager.GetUserId(User);
            var id1 = int.Parse(userId);
            var model = await _TaskUserAppServices.GetT(id, cToken);
            if (model == null || model.UserTaId != id1)
                return NotFound();
            await _TaskUserAppServices.DeleteT(id, cToken);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cToken)
        {
            var userId = _userManager.GetUserId(User);
            var id1 = int.Parse(userId);
            var model = await _TaskUserAppServices.GetT(id, cToken);
            if (model == null || model.UserTaId != id1)
                return NotFound();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TaskU model, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _TaskUserAppServices.UpdateT(model, cToken);
                if (result.IsSuccess)
                {
                    ViewBag.SuccessMessage = result.IsMessage;

                }
                else
                {
                    ViewBag.ErrorMessage = result.IsMessage;
                }
                return RedirectToAction(nameof(Index));
            }
                
            return View(model);


        }
        [HttpGet]
        public async Task<IActionResult> EndTask(int id, CancellationToken cToken)
        {
            var userId = _userManager.GetUserId(User);
            var id1 = int.Parse(userId);
            var model = await _TaskUserAppServices.GetT(id, cToken);
            if (model == null || model.UserTaId != id1)
                return NotFound();
            var result = await _TaskUserAppServices.EndTask(id, cToken);
            if (result.IsSuccess)
            {

                ViewBag.SuccessMessage = result.IsMessage;

            }
            else
            {
                ViewBag.ErrorMessage = result.IsMessage;

            }
            return RedirectToAction(nameof(Index));
        }
       
    }
}
