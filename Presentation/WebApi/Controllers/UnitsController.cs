using Dto;
using Microsoft.AspNetCore.Mvc;
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

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var classes = _unitService.GetAll();
            return Ok(classes);
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(string id)
        //{
        //    var unit = _unitService.GetById(id);

        //    return Ok(unit);
        //}

        //[HttpPost]
        //public IActionResult Post([FromBody] UnitDto unit)
        //{
        //    _unitService.Insert(unit);

        //    return Ok();
        //}

        //[HttpPut]
        //public IActionResult Put([FromBody] UnitDto unit)
        //{
        //    _unitService.Update(unit);

        //    return Ok();
        //}

        [HttpDelete("id")]
        public IActionResult Delete(string id)
        {
            return Ok();
        }
    }
}
