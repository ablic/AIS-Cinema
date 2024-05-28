﻿using System.ComponentModel.DataAnnotations;

namespace AIS_Cinema.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;
    }
}
