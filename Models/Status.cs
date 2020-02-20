using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class Status
    {
        public Status()
        {
            Fighter = new HashSet<Fighter>();
        }

        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public int Id { get; set; }
        [Display(Name = "Статус")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Введіть від 3 до 40 символів")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public string Name { get; set; }

        public virtual ICollection<Fighter> Fighter { get; set; }
    }
}
