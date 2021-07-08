using org.mariuszgromada.math.mxparser;
using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using static RPGTest.Core.Domain.Enums;

namespace RPGTest.Core.Services
{
    public class UnitService : IUnitService
    {
        private readonly ICoordinatesService _coordinatesService;
        private readonly IMongoDBRepository<Unit> _unitRepository;
        private readonly IUnitClassService _unitClassService;

        public UnitService(ICoordinatesService coordinatesService, IMongoDBRepository<Unit> unitRepository, IUnitClassService unitClassService)
        {
            _coordinatesService = coordinatesService;
            _unitRepository = unitRepository;
            _unitClassService = unitClassService;
        }

        public void Attack(Unit attacker, Unit attackedUnit)
        {
            bool canAttack = CanAttack(attacker.Id, attackedUnit.Id);

            if (canAttack)
            {
                var attackerClass = _unitClassService.GetById(attacker.ClassId);

                var damage = attackerClass.AttackType switch
                {
                    AttackType.Melee => attackerClass.BaseDamage,
                    AttackType.Range => attackerClass.BaseDamage,
                    AttackType.Magical => attackerClass.BaseDamage,
                    _ => throw new NotImplementedException()
                };
            }
        }

        public bool CanAttack(string attackerUnitId, string attackedUnitId)
        {
            var attackerUnit = _unitRepository.GetById(attackerUnitId);
            var attackedUnit = _unitRepository.GetById(attackedUnitId);

            var distance = _coordinatesService.CalculateDistance(x1: attackerUnit.X,
                                                                 y1: attackerUnit.Y,
                                                                 x2: attackedUnit.X,
                                                                 y2: attackedUnit.Y);

            var attackerAttackType = _unitClassService.GetById(attackerUnit.ClassId).AttackType;

            bool canAttack = attackerAttackType switch
            {
                AttackType.Melee => 10 > distance,
                AttackType.Range => 350 > distance,
                AttackType.Magical => 150 > distance,
                _ => throw new NotImplementedException()
            };

            return canAttack;
        }

        public int CalculateDamage(string unitId)
        {
            var unit = _unitRepository.GetById(unitId);
            var unitClass = _unitClassService.GetById(unit.ClassId);

            if (!string.IsNullOrEmpty(unitClass.Condition))
            {
                Expression expression = new Expression(unitClass.Condition);
                bool result = Convert.ToBoolean(expression.calculate());

                if (result)
                {
                    Expression expression1 = new Expression(unitClass.Formula);
                    return expression1.calculate();
                }
                else
                {
                    Expression expression1 = new Expression(unitClass.Formula2);
                }
            }

            return 0;
        }

        public void Delete(Unit unit)
        {
            throw new NotImplementedException();
        }

        public ICollection<Unit> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Unit unit)
        {
            throw new NotImplementedException();
        }

        public void Move(string unitId, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Update(Unit unit)
        {
            throw new NotImplementedException();
        }

    }
}
