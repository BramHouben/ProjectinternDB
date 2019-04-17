﻿using System.ComponentModel.DataAnnotations;

namespace Model.Onderwijsdelen
{
    public class Traject 
    {
        [Key]
        public int TrajectId { get; set; }
        public string TrajectNaam { get; set; }

        public int Prioriteit { get; set; }
    }
}
