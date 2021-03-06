using RPGTest.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTest.Core.Services
{
    public interface IUnitService
    {
        public ICollection<Unit> GetAll();
        public void Insert(Unit unit);
        public void Update(Unit unit);
        public void Delete(Unit unit);
        public Unit GetById(string id);
        public void Attack(Unit attacker, Unit attackedUnit);
        public void ExecActionAfterCondition(string unitId, double distance);
        public void Move(string unitId, int x, int y);
        public bool CanAttack(string attackerUnitId, string attackedUnitId);
    }
}
