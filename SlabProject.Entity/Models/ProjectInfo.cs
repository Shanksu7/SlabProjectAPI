﻿using System;

namespace SlabProject.Entity.Models
{
    public class ProjectInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime? FinishedDate { get; set; }
    }
}