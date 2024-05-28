using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Models
{
    public class EmailEntry
    {
        [Display(Name = "Электронная почта, на которую пришлем билеты")]
        [Required(ErrorMessage = "Укажите почту для отправки билетов")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        public string Email { get; set; }
    }
}
