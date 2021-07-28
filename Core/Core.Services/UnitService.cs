using org.mariuszgromada.math.mxparser;
using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void Attack(Unit attackerUnit, Unit attackedUnit)
        {
            bool canAttack = CanAttack(attackerUnit.Id, attackedUnit.Id);

            if (canAttack)
            {
                var damage = CalculateDamage(attackerUnit.Id, attackedUnit.Id);

                var x = attackedUnit.HP - damage;

                attackedUnit.HP = x >= 0 ? x : 0;

                Update(attackedUnit);

                var distance = _coordinatesService.CalculateDistance(x1: attackerUnit.X,
                                                                     y1: attackerUnit.Y,
                                                                     x2: attackedUnit.X,
                                                                     y2: attackedUnit.Y);

                ExecActionAfterCondition(attackerUnit.Id, distance);
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

        public int CalculateDamage(string attackerUnitId, string attackedUnitId)
        {
            var attackerUnit = _unitRepository.GetById(attackerUnitId);
            var attackerUnitClass = _unitClassService.GetById(attackerUnit.ClassId);

            var attackedUnit = _unitRepository.GetById(attackedUnitId);
            var attackedUnitClass = _unitClassService.GetById(attackedUnit.ClassId);

            double distance = _coordinatesService.CalculateDistance(x1: attackerUnit.X,
                                                                    y1: attackerUnit.Y,
                                                                    x2: attackedUnit.X,
                                                                    y2: attackedUnit.Y);

            int damageWithoutResist;

            Expression formulaExpression = new Expression(SetFormulaVariables(attackerUnitClass.Formula, attackerUnit, attackerUnitClass, distance));

            if (string.IsNullOrEmpty(attackerUnitClass.Condition))
            {
                damageWithoutResist = (int)formulaExpression.calculate();
            }
            else
            {
                Expression conditionExpression = new Expression(SetFormulaVariables(attackerUnitClass.Condition, attackerUnit, attackerUnitClass, distance));
                bool result = Convert.ToBoolean(conditionExpression.calculate());

                if (result)
                {
                    damageWithoutResist = (int)formulaExpression.calculate();
                }
                else
                {
                    Expression formula2Expression = new Expression(SetFormulaVariables(attackerUnitClass.Formula2, attackerUnit, attackerUnitClass, distance));
                    damageWithoutResist = (int)formula2Expression.calculate();
                }
            }

            int damage = attackerUnitClass.DamageType switch
            {
                DamageType.Physical => damageWithoutResist - attackedUnit.Armor,
                DamageType.Magical => damageWithoutResist - attackedUnit.MagicResist,
                _ => throw new InvalidEnumArgumentException()
            };

            return Math.Max(0, damage);
        }

        public void ExecActionAfterCondition(string unitId, double distance)
        {
            var unit = _unitRepository.GetById(unitId);
            var unitClass = _unitClassService.GetById(unit.ClassId);

            Expression expression = new Expression(SetFormulaVariables(unitClass.Condition, unit, unitClass, distance));
            bool result = Convert.ToBoolean(expression.calculate());

            if (result)
            {
                var postConditionExpression = new Expression(SetFormulaVariables(unitClass.PostTrueConditionAction, unit, unitClass, distance));
                var fieldNewValue = (int)postConditionExpression.calculate();

                _ = unitClass.TrueConditionActionChangeableProperty switch
                {
                    "ТекущееЗдоровье" => unit.HP = fieldNewValue <= unit.MaxHP && fieldNewValue >= 0 ? fieldNewValue : throw new Exception(nameof(fieldNewValue)),
                    "ТекущаяМана" => unit.Mana = fieldNewValue <= unit.MaxMana && fieldNewValue >= 0 ? fieldNewValue : throw new Exception(nameof(fieldNewValue)),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                var postConditionExpression = new Expression(SetFormulaVariables(unitClass.PostFalseConditionAction, unit, unitClass, distance));
                var fieldNewValue = (int)postConditionExpression.calculate();

                _ = unitClass.FalseConditionActionChangeableProperty switch
                {
                    "ТекущееЗдоровье" => unit.HP = fieldNewValue <= unit.MaxHP && fieldNewValue >= 0 ? fieldNewValue : throw new Exception(nameof(fieldNewValue)),
                    "ТекущаяМана" => unit.Mana = fieldNewValue <= unit.MaxMana && fieldNewValue >= 0 ? fieldNewValue : throw new Exception(nameof(fieldNewValue)),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            Update(unit);
        }

        private string SetFormulaVariables(string condition, Unit unit, UnitClass unitClass, double distance)
        {
            if(condition == null)
            {
                return string.Empty;
            }

            string result = string.Empty;

            //result = Regex.Replace(condition, @"БазовыйУрон", unitClass.BaseDamage.ToString());
            //result = Regex.Replace(condition, "НедостающееЗдоровье", (unit.MaxHP - unit.HP).ToString());
            //result = Regex.Replace(condition, "МаксимальноеЗдоровье", unit.MaxHP.ToString());
            //result = Regex.Replace(condition, "ДистанцияДоЦели", distance.ToString());
            //result = Regex.Replace(condition, "РадиусАтаки", ((int)unitClass.AttackType).ToString());
            //result = Regex.Replace(condition, "ТекущаяМана", unit.Mana.ToString());
            //result = Regex.Replace(condition, "ОкруглитьВБольшуюСторону", "ceil");
            //result = Regex.Replace(condition, "ОкруглитьВБольшуюСторону", "floor");

            result = Regex.Replace(condition, @"\bБазовыйУрон\b", unitClass.BaseDamage.ToString());
            result = Regex.Replace(result, @"\bНедостающееЗдоровье\b", (unit.MaxHP - unit.HP).ToString());
            result = Regex.Replace(result, @"\bМаксимальноеЗдоровье\b", unit.MaxHP.ToString());
            result = Regex.Replace(result, @"\bДистанцияДоЦели\b", distance.ToString());
            result = Regex.Replace(result, @"\bРадиусАтаки\b", ((int)unitClass.AttackType).ToString());
            result = Regex.Replace(result, @"\bТекущаяМана\b", unit.Mana.ToString());
            result = Regex.Replace(result, @"\bОкруглитьВБольшуюСторону\b", "ceil");
            result = Regex.Replace(result, @"\bОкруглитьВБольшуюСторону\b", "floor");
            result = Regex.Replace(result, @"\s+", "");
            return result;
        }

        public void Delete(Unit unit)
        {
            _unitRepository.Delete(unit);
        }

        public ICollection<Unit> GetAll()
        {
            return _unitRepository.Get();
        }

        public void Insert(Unit unit)
        {
            _unitRepository.Insert(unit);
        }

        public void Move(string unitId, int x, int y)
        {
            var unit = GetById(unitId);

            unit.X = x;
            unit.Y = y;

            Update(unit);
        }

        public void Update(Unit unit)
        {
            _unitRepository.Update(unit);
        }

        public Unit GetById(string id)
        {
            var unit = _unitRepository.GetById(id);

            return unit;
        }
    }
}
