﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SlabProject.Entity.Requests
{
    public class CreateProjectRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }
    }
}