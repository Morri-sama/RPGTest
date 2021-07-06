using Microsoft.AspNetCore.Mvc;
using RPGTest.Core.Domain;
using RPGTest.Core.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitClassesController : ControllerBase
    {
        private readonly IUnitClassService _unitClassService;

        public UnitClassesController(IUnitClassService unitClassService)
        {
            _unitClassService = unitClassService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var classes = _unitClassService.GetAll();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var unitClass = _unitClassService.GetById(id);

            return Ok(unitClass);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UnitClass unitClass)
        {
            _unitClassService.Insert(unitClass);

            return Ok();
        }

        [HttpPost]
        public IActionResult Put([FromBody] UnitClass unitClass)
        {
            _unitClassService.Update(unitClass);

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult Delete(string id)
        {
            return Ok();
        }
    }
}
