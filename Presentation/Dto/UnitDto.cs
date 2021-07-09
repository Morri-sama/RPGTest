using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class UnitDto : BaseDto
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int Armor { get; set; }
        public int MagicResist { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string ClassId { get; set; }
    }
}
