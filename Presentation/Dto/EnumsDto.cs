using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class EnumsDto
    {
        public enum AttackType { Melee = 10, Range = 350, Magical = 150 }
        public enum DamageType { Physical = 0, Magical = 1 }
    }
}
