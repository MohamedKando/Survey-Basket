﻿using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class BaseEntity
    {
        [Key]

        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}
