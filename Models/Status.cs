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

        public int Id { get; set; }
        [Display(Name = "Статус")]
        public string Name { get; set; }

        public virtual ICollection<Fighter> Fighter { get; set; }
    }
}
