using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Display(Name = "Фильм")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Display(Name = "Дата и время")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Зал")]
        public int HallId { get; set; }
        public Hall? Hall { get; set; }

        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Минимальная стоимость билета")]
        public decimal MinPrice { get; set; }

        [JsonIgnore]
        public List<Ticket> Tickets { get; set; } = new();
    }
}
