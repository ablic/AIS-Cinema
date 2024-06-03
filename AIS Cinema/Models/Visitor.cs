using Microsoft.AspNetCore.Identity;

namespace AIS_Cinema.Models
{
    public class Visitor : IdentityUser
    {
        public long? TelegramChatId { get; set; }
    }
}