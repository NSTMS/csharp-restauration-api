using AutoMapper;
using Microsoft.EntityFrameworkCore;
using restAPI.Entities;
using restAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace restAPI.Services
{
    public class DishesService : IDishesService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishesService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<DishDto> GetAllDishes(int id)
        {
            var restaurant = _dbContext
                   .Restaurants
                   .Include(r => r.Dishes)
                   .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                return null;

            var dishes = restaurant.Dishes.ToList();

            var dishDtos = _mapper.Map<List<DishDto>>(dishes);
            return dishDtos;
        }
        public DishDto GetById(int id, int dishId)
        {
            var restaurant = _dbContext
               .Restaurants
               .Include(r => r.Dishes)
               .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                return null;


            var dishes = restaurant.Dishes.ToList();
            var dish = dishes.FirstOrDefault(x => x.Id == dishId);

            if (dish == null)
                return null;

            var result = _mapper.Map<DishDto>(dish);
            return result;

        }

        public bool AddToDishes(int id, CreateDishDto dto)
        {
            var dish = _mapper.Map<Dish>(dto);
            try
            {
                _dbContext
                    .Restaurants
                    .Include(r => r.Dishes)
                    .FirstOrDefault(x => x.Id == id)
                    .Dishes.Add(dish);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
