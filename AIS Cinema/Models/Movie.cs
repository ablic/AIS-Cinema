using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 600, ErrorMessage = "Укажите корректную продолжительность в минутах")]
        [Display(Name = "Продолжительность в минутах")]
        public int Duration { get; set; }

        [Range(1850, 2100, ErrorMessage = "Укажите год производства в промежутке от 1850 до 2100")]
        [Display(Name = "Год производства")]
        public int ProductionYear { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата выхода в прокат")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Возрастное ограничение")]
        public int AgeLimitId { get; set; }
        public AgeLimit? AgeLimit { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }
        public string? PosterPath { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public List<Country> Countries { get; set; } = new();

        [JsonIgnore]
        public List<Session> Sessions { get; set; } = new();
    }
}