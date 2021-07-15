using org.mariuszgromada.math.mxparser;
using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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

                var distance = _coordinatesService.CalculateDistance(x1: attacker.X,
                                                                     y1: attacker.Y,
                                                                     x2: attackedUnit.X,
                                                                     y2: attackedUnit.Y);

                var damage = CalculateDamage(attacker.Id, distance);

                var x = attackedUnit.HP - damage;

                attackedUnit.HP = x >= 0 ? x : 0;

                Update(attackedUnit);
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

            return (int)attackerAttackType >= distance;
        }

        public int CalculateDamage(string unitId, double distance)
        {
            var unit = _unitRepository.GetById(unitId);
            var unitClass = _unitClassService.GetById(unit.ClassId);

            Expression formulaExpression = new Expression(SetFormulaVariables(unitClass.Formula, unit, unitClass, distance));

            if (!string.IsNullOrEmpty(unitClass.Condition))
            {

                Expression conditionExpression = new Expression(SetFormulaVariables(unitClass.Condition, unit, unitClass, distance));
                bool result = Convert.ToBoolean(conditionExpression.calculate());

                if (result)
                {
                    return (int)formulaExpression.calculate();
                }
                else
                {
                    Expression formula2Expression = new Expression(SetFormulaVariables(unitClass.Formula2, unit, unitClass, distance));
                    return (int)formula2Expression.calculate();
                }
            }

            return (int)formulaExpression.calculate();
        }

        private void ExecActionAfterCondition(string unitId, double distance)
        {
            var unit = _unitRepository.GetById(unitId);
            var unitClass = _unitClassService.GetById(unit.ClassId);

            var trueProperty = unit.GetType().GetProperty(unitClass.TrueConditionActionChangeableProperty);
            var falseProperty = unit.GetType().GetProperty(unitClass.FalseConditionActionChangeableProperty);

            Expression expression = new Expression(SetFormulaVariables(unitClass.Condition, unit, unitClass, distance));
            bool result = Convert.ToBoolean(expression.calculate());

            if (result)
            {
                //trueProperty.set
            }
            else
            {

            }
        }

        private string SetFormulaVariables(string condition, Unit unit, UnitClass unitClass, double distance)
        {
            string result = string.Empty;

            result = Regex.Replace(condition, "БазовыйУрон", unitClass.BaseDamage.ToString());
            result = Regex.Replace(condition, "НедостающееЗдоровье", (unit.MaxHP - unit.HP).ToString());
            result = Regex.Replace(condition, "МаксимальноеЗдоровье", unit.MaxHP.ToString());
            result = Regex.Replace(condition, "ДистанцияДоЦели", distance.ToString());
            result = Regex.Replace(condition, "РадиусАтаки", ((int)unitClass.AttackType).ToString());
            result = Regex.Replace(condition, "ТекущаяМана", unit.Mana.ToString());
            return result;
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
