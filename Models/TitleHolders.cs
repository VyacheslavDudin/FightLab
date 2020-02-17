using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class TitleHolders
    {
        [Display(Name = "Власник титулу")]
        public int FighterId { get; set; }
        [Display(Name = "Титул")]
        public int TitleId { get; set; }
        [Display(Name = "Дата отримання")]
        public DateTime DateOfGettingTitle { get; set; }

        public virtual Fighter Fighter { get; set; }
        public virtual Title Title { get; set; }
    }
}
