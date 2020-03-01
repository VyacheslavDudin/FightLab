using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class Country
    {
        public Country()
        {
            Fighter = new HashSet<Fighter>();
        }

        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public int Id { get; set; }

        [Display(Name = "Країна")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Введіть від 2 до 40 символів")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public string Name { get; set; }

        public virtual ICollection<Fighter> Fighter { get; set; }
    }
}
