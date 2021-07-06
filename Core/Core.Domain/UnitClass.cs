using System;
using System.Collections.Generic;
using System.Text;
using static Core.Domain.Enums;

namespace Core.Domain
{
    public class UnitClass : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public AttackType AttackType { get; set; }
        public DamageType DamageType { get; set; }
    }
}
