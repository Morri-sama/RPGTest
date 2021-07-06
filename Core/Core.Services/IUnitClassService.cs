using RPGTest.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTest.Core.Services
{
    public interface IUnitClassService
    {
        public ICollection<UnitClass> GetAll();
        public void Insert(UnitClass unitClass);
        public void Update(UnitClass unitClass);
        public void Delete(UnitClass unitClass);
        public UnitClass GetById(string id);
    }
}
