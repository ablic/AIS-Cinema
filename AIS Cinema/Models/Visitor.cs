using Microsoft.AspNetCore.Identity;

namespace AIS_Cinema.Models
{
    public class Visitor : IdentityUser
    {
        public List<Ticket> Tickets { get; set; } = new();
    }
}
