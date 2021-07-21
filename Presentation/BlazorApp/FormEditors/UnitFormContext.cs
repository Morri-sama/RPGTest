using Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.FormEditors
{
    public class UnitFormContext : FormContextBase<UnitDto>
    {
        [Required]
        public int HP { get; set; }

        [Required]
        public int MaxHP { get; set; }

        [Required]
        public int Mana { get; set; }

        [Required]
        public int MaxMana { get; set; }

        [Required]
        public int Armor { get; set; }

        [Required]
        public int MagicResist { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

        [Required]
        public string ClassId { get; set; }
    }
}
