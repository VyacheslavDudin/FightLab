using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WebApplication2
{
    public partial class Title
    {
        public int Id { get; set; }
        [Display(Name = "Титул")]
        public string Name { get; set; }

        public virtual TitleHolders TitleHolders { get; set; }
    }
}
