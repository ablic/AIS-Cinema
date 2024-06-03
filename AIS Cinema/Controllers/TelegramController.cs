using AIS_Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AIS_Cinema.Controllers
{
    public class TelegramController : Controller
    {
        private readonly UserManager<Visitor> _userManager;
        private readonly SignInManager<Visitor> _signInManager;

        public TelegramController(UserManager<Visitor> userManager, SignInManager<Visitor> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("telegram/login/{chatId}")]
        public IActionResult Login(long chatId)
        {
            TempData["ChatId"] = chatId.ToString();
            return View(new TelegramLoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(TelegramLoginModel model)
        {
            var chatId = long.Parse(TempData["ChatId"].ToString());

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                user.TelegramChatId = chatId;
                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Success));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}
