using AIS_Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramAuthController : ControllerBase
    {
        private readonly AISCinemaDbContext _context;
        private readonly UserManager<Visitor> _userManager;

        public TelegramAuthController(AISCinemaDbContext context, UserManager<Visitor> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("isAuthenticated/{chatId}")]
        public async Task<ActionResult<bool>> IsAuthenticated(long chatId)
        {
            return Ok(await _context.Users
                .FirstOrDefaultAsync(u => u.TelegramChatId == chatId) != null);
        }

        [HttpGet("getUserId/{chatId}")]
        public async Task<ActionResult<string>> GetUserId(long chatId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.TelegramChatId == chatId);
            return Ok(user.Id);
        }
    }
}
