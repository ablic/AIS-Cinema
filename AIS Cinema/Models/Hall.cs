using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Models
{
    public class Hall
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string Schema { get; set; } = string.Empty;
    }
}
