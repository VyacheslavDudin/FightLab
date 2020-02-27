using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class Fight
    {
        public int Id { get; set; }

        [Display(Name = "Дата бою")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        [Display(Name = "Боєць 1")]
        public int Fighter1Id { get; set; }
        [Display(Name = "Боєць 2")]
        public int Fighter2Id { get; set; }
        [Display(Name = "Результат")]
        [Required(ErrorMessage = "Введіть результат бою")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Введіть від 3 до 40 символів")]
        public string Winner { get; set; }
        [StringLength(40, ErrorMessage = "Введіть до 40 символів")]
        [Required(ErrorMessage ="Введіть тип результату")]
        [Display(Name = "Тип перемоги")]
        public string TypeOfWin { get; set; }

        [Display(Name = "Боєць 1")]
        public virtual Fighter Fighter1 { get; set; }
        [Display(Name = "Боєць 2")]
        public virtual Fighter Fighter2 { get; set; }
    }
}
