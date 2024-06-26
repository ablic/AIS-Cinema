﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AIS_Cinema.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Полное название")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Сокращенное название")]
        public string? ShortName { get; set; }

        [JsonIgnore]
        public List<Movie> Movies { get; set; } = new();
    }
}
