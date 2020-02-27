using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.Routing;



namespace WebApplication2
{
    [MetadataType(typeof(Division))]
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
        //[System.Web.Mvc.Remote("NameExist", "RemoteValidation", HttpMethod = "POST", ErrorMessage = "Таке ім\'я вже існує")]
        public string Name { get; set; }

        public virtual ICollection<Fighter> Fighter { get; set; }
    }
}
