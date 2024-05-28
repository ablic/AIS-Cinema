using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIS_Cinema.Models
{
    public class Ticket
    {
        public int Id { get; set; }


        [Display(Name = "Сеанс")]
        public int SessionId { get; set; }
        public Session? Session { get; set; }

        [Display(Name = "Ряд")]
        public int RowNumber { get; set; }

        [Display(Name = "Место")]
        public int SeatNumber { get; set; }


        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Владелец")]
        public string? VisitorId { get; set; }
        public Visitor? Visitor { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Электронная почта для отправки билетов")]
        public string? OwnerEmail { get; set; }

        [NotMapped]
        public bool IsBought => false;
    }
}
