using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class TitleHolders
    {
        [Display(Name = "Власник титулу")]
        //[Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public int? FighterId { get; set; }
        [Display(Name = "Титул")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public int TitleId { get; set; }
        [Display(Name = "Дата отримання")]
        [DataType(DataType.Date)]
        public DateTime? DateOfGettingTitle { get; set; }

        [Display(Name = "Власник титулу")]
        //[Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public virtual Fighter? Fighter { get; set; }
        [Display(Name = "Титул")]
        public virtual Title Title { get; set; }
    }
}
