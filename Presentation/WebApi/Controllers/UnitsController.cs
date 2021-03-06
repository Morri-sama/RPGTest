using AutoMapper;
using Dto;
using Microsoft.AspNetCore.Mvc;
using RPGTest.Core.Domain;
using RPGTest.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitsController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var units = _unitService.GetAll();

            var unitDtos = _mapper.Map<List<UnitDto>>(units);

            return Ok(unitDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var unit = _unitService.GetById(id);
            var unitDto = _mapper.Map<UnitDto>(unit);

            return Ok(unitDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UnitDto unitDto)
        {
            var unit = _mapper.Map<Unit>(unitDto);
            _unitService.Insert(unit);

            return CreatedAtAction("Get", new { id = unit.Id }, unit);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UnitDto unitDto)
        {
            var unit = _mapper.Map<Unit>(unitDto);
            _unitService.Update(unit);

            unitDto = _mapper.Map<UnitDto>(unit);

            return Ok(unit);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var unit = _unitService.GetById(id);

            if (unit is null)
            {
                return NotFound();
            }

            _unitService.Delete(unit);

            return NoContent();
        }

        [HttpGet("{attackerUnitId}/attack/{attackedUnitId}")]
        public IActionResult Attack(string attackerUnitId, string attackedUnitId)
        {
            if (string.IsNullOrEmpty(attackerUnitId))
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(attackedUnitId))
            {
                return BadRequest();
            }

            if(attackerUnitId == attackedUnitId)
            {
                return BadRequest("Два одинаковых ID");
            }

            bool canAttack = _unitService.CanAttack(attackerUnitId, attackedUnitId);

            if (!canAttack)
            {
                return BadRequest("Нельзя атаковать");
            }

            var attackerUnit = _unitService.GetById(attackerUnitId);
            var attackedUnit = _unitService.GetById(attackedUnitId);

            _unitService.Attack(attackerUnit, attackedUnit);

            return NoContent();
        }

        [HttpGet("{attackerUnitId}/canattack/{attackedUnitId}")]
        public IActionResult CanAttack(string attackerUnitId, string attackedUnitId)
        {
            if (string.IsNullOrEmpty(attackerUnitId))
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(attackedUnitId))
            {
                return BadRequest();
            }

            bool canAttack = _unitService.CanAttack(attackerUnitId, attackedUnitId);

            return Ok(canAttack);
        }
    }
}
