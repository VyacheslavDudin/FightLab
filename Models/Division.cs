using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class Division
    {
        public Division()
        {
            Fighter = new HashSet<Fighter>();
        }

        [Required(ErrorMessage ="Поле не повинно бути порожнім!")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Введіть від 3 до 40 символів")]
        [Display(Name="Вагова категорія")]
        public string Name { get; set; }

        public virtual ICollection<Fighter> Fighter { get; set; }
    }
}
