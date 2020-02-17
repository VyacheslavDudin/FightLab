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

        public int Id { get; set; }

        [Display(Name = "Країна")]
        public string Name { get; set; }

        public virtual ICollection<Fighter> Fighter { get; set; }
    }
}
