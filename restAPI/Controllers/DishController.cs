using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restAPI.Entities;
using restAPI.Models;
using restAPI.Services;

namespace restAPI.Controllers
{
    [Route("api/restaurant/{id}/dishes")]
    public class DishController : ControllerBase
    {
        private readonly IDishesService _dishesService;

        public DishController(IDishesService dishesService)
        {
            _dishesService = dishesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DishDto>> GetDishes([FromRoute] int id)
        {
            var dishes = _dishesService.GetAllDishes(id);
            if (dishes is null)
            {
                return NotFound("Nie znaleziono");
            }
            return Ok(dishes);
        }
        [HttpPost]
        public ActionResult<IEnumerable<DishDto>> AddDish([FromRoute] int id, [FromBody] CreateDishDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dishesService.AddToDishes(id, dto);
            return Created($"utworzony poprawnie", null);
        }
        [HttpGet("{dishId}")]
        public ActionResult<IEnumerable<DishDto>> GetDishesById([FromRoute] int id, [FromRoute] int dishId)
        {
            var dish = _dishesService.GetById(id, dishId);
            if (dish is null)
            {
                return NotFound("Nie znaleziono");
            }
            return Ok(dish);
        }

    }
}
