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
        [Display(Name = "Зріст")]
        public int? Height { get; set; }
        [Display(Name = "Вага")]
        public int? Weight { get; set; }
        [Display(Name = "Дебют")]
        [DataType(DataType.Date)]
        public DateTime? Debut { get; set; }
        [Display(Name = "П.І.Б.")]
        public string Name { get; set; }

        [Display(Name = "Країна")]
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
