using System;
using System.Collections.Generic;
using System.Text;
using static RPGTest.Core.Domain.Enums;

namespace RPGTest.Core.Domain
{
    public class Unit : EntityBase
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
