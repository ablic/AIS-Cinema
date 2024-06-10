using AIS_Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AIS_Cinema.Controllers
{
    public class TelegramBindingController : Controller
    {
        private readonly UserManager<Visitor> _userManager;
        private readonly SignInManager<Visitor> _signInManager;

        public TelegramBindingController(UserManager<Visitor> userManager, SignInManager<Visitor> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("telegramBinding/login/{chatId}")]
        public async Task<IActionResult> Login(long chatId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                user.TelegramChatId = chatId;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Success));
                }
            }

            TempData["ChatId"] = chatId.ToString();
            return View(new TelegramLoginModel());
        }

        [HttpPost]
        [Route("telegramBinding/login/{chatId}")]
        public async Task<IActionResult> Login(TelegramLoginModel model)
        {
            var chatId = long.Parse(TempData["ChatId"].ToString());

            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                user.TelegramChatId = chatId;
                var result2 = await _userManager.UpdateAsync(user);

                if (result2.Succeeded)
                {
                    return RedirectToAction(nameof(Success));
                }
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
