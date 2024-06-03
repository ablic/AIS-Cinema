using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramAuthController : ControllerBase
    {
        private readonly AISCinemaDbContext _context;

        public TelegramAuthController(AISCinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult<bool>> IsAuthenticated(long chatId)
        {
            return Ok(await _context.Users
                .FirstOrDefaultAsync(u => u.TelegramChatId == chatId) != null);
        }
    }
}
