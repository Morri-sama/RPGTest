using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dto.EnumsDto;

namespace BlazorApp.FormEditors
{
    public class UnitClassFormContext : FormContextBase<UnitClassDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseDamage { get; set; }
        public string Formula { get; set; }
        public string Formula2 { get; set; }
        public string Condition { get; set; }
        public string PostTrueConditionAction { get; set; }
        public string PostFalseConditionAction { get; set; }
        public string TrueConditionActionChangeableProperty { get; set; }
        public string FalseConditionActionChangeableProperty { get; set; }
        public AttackType AttackType { get; set; } = AttackType.Melee;
        public DamageType DamageType { get; set; } = DamageType.Physical;
    }
}
