using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restAPI.Entities;
using restAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace restAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController: ControllerBase
    {

        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return Created($"/api/restaurants/{restaurant.Id}",null);
        }



        [HttpGet]
        public ActionResult<IEnumerable<RestaurantsDto>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();
            /*    var restaurantsDtos = restaurants.Select(r => new RestaurantsDto()
                {
                    Name = r.Name,
                    Category = r.Category,
                    City = r.Address.City,
                    ...
                });*/
            var restaurantsDtos = _mapper.Map<List<RestaurantsDto>>(restaurants);

            return Ok(restaurantsDtos);
        }


        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RestaurantsDto>> Get([FromRoute]int id)
        {
            var restaurant = _dbContext
               .Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .FirstOrDefault(r => r.Id == id);

            //data transfer object (DTO)

            if(restaurant is null)
            {
                return NotFound("Nie znaleziono");
            }
  
            var restaurantDto = _mapper.Map<RestaurantsDto>(restaurant);
            return Ok(restaurantDto);
        }

    }
}
