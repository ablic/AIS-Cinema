using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Models
{
    public class TelegramLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
