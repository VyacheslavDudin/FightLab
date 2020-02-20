using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public partial class Fighter
    {
        public Fighter()
        {
            FightFighter1 = new HashSet<Fight>();
            FightFighter2 = new HashSet<Fight>();
            TitleHolders = new HashSet<TitleHolders>();
        }

        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public int Id { get; set; }
        [Display(Name = "Вагова категорія")]
        public int DivisionId { get; set; }
        [Display(Name = "Дата народж.")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Країна")]
        public int CountryId { get; set; }
        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        [Range(100, 250, ErrorMessage = "Недостовірні дані!")]
        [Display(Name = "Зріст")]
        public int? Height { get; set; }
        [Range(40, 350, ErrorMessage = "Недостовірні дані!")]
        [Display(Name = "Вага")]
        public int? Weight { get; set; }
        [Display(Name = "Дебют")]
        [DataType(DataType.Date)]
        public DateTime? Debut { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        [Display(Name = "П.І.Б.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Введіть від 3 до 40 символів")]
        public string Name { get; set; }

        [Display(Name = "Країна")]
        //[Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        public virtual Country Country { get; set; }
        [Display(Name = "Ваг. кат.")]
        public virtual Division Division { get; set; }
        [Display(Name = "Статус")]
        public virtual Status Status { get; set; }
        public virtual ICollection<Fight> FightFighter1 { get; set; }
        public virtual ICollection<Fight> FightFighter2 { get; set; }
        public virtual ICollection<TitleHolders> TitleHolders { get; set; }
    }
}
