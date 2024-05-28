using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Areas.Admin.Models
{
    public class SessionPrimaryData
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Укажите дату проведения сеанса")]
        [Display(Name = "Дата и время")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Зал")]
        public int HallId { get; set; }

        [Display(Name = "Минимальная цена")]
        public decimal MinPrice { get; set; }
    }
}
