using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dto.EnumsDto;

namespace Dto
{
    public class UnitClassDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseDamage { get; set; }
        public string Formula { get; set; }
        public string Formula2 { get; set; }
        public string Condition { get; set; }
        public string PostTrueConditionAction { get; set; }
        public string PostFalseConditionAction { get; set; }
        public string ConditionActionPropertyName { get; set; }
        public AttackType AttackType { get; set; }
        public DamageType DamageType { get; set; }
    }
}
