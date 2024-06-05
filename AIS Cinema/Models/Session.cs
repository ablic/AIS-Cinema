using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIS_Cinema.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime DateTime { get; set; }
        public int HallId { get; set; }
        public Hall? Hall { get; set; }

        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal MinPrice { get; set; }

        [JsonIgnore]
        public List<Ticket> Tickets { get; set; } = new();
    }
}
