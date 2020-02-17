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

        public int Id { get; set; }
        [Display(Name="Вагова категорія")]
        public string Name { get; set; }

        public virtual ICollection<Fighter> Fighter { get; set; }
    }
}
