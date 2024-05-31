using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Models
{
    public class Payment
    {
        [Required(ErrorMessage = "Номер карты обязателен для заполнения")]
        [CreditCard(ErrorMessage = "Некорректный номер карты")]
        [Display(Name = "Номер карты")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Имя владельца карты обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Имя владельца карты не должно превышать 100 символов")]
        [Display(Name = "Владелец карты")]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = "Срок действия обязателен для заполнения")]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "Некорректный формат срока действия (MM/YY)")]
        [Display(Name = "Срок действия")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV обязателен для заполнения")]
        [RegularExpression(@"\d{3}", ErrorMessage = "Некорректный CVV код")]
        [StringLength(3, ErrorMessage = "CVV код должен содержать 3 цифры")]
        [Display(Name = "CVV")]
        public string CVV { get; set; }
    }
}
