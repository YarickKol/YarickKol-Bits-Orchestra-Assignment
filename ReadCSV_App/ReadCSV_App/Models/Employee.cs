﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ReadCSV_App.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Married { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }

    }
}
