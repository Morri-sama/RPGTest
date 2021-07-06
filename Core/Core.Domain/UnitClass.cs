using System;
using System.Collections.Generic;
using System.Text;
using static RPGTest.Core.Domain.Enums;

namespace RPGTest.Core.Domain
{
    public class UnitClass : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public AttackType AttackType { get; set; }
        public DamageType DamageType { get; set; }
    }
}
