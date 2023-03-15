using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restAPI.Entities;
using restAPI.Models;
using restAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace restAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]

        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _restaurantService.Create(dto);
            return Created($"/api/restaurant/{id}", null);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _restaurantService.Delete(id);
            if (isDeleted) return NoContent();
            else return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Update([FromBody]ToModify obj, [FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isModifyied = _restaurantService.Modify(obj,id);
            if (isModifyied) return Ok("Zaktualizowano pomyślnie");
            else return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantsDto>> GetAll()
        { 
            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "HasNationality")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RestaurantsDto>> Get([FromRoute]int id)
        {
            var restaurant = _restaurantService.GetById(id);
            if(restaurant is null)
            {
                return NotFound("Nie znaleziono");
            }
            return Ok(restaurant);
        }
    
    }
}
