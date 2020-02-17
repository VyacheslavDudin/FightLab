﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class Fight
    {
        public int Id { get; set; }

        [Display(Name = "Дата бою")]
        public DateTime? Date { get; set; }
        [Display(Name = "Боєць 1")]
        public int Fighter1Id { get; set; }
        [Display(Name = "Боєць 2")]
        public int Fighter2Id { get; set; }
        [Display(Name = "Переможець")]
        public string Winner { get; set; }
        [Display(Name = "Тип перемоги")]
        public string TypeOfWin { get; set; }

        public virtual Fighter Fighter1 { get; set; }
        public virtual Fighter Fighter2 { get; set; }
    }
}