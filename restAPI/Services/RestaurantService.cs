using AutoMapper;
using Microsoft.EntityFrameworkCore;
using restAPI.Entities;
using restAPI.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.SymbolStore;
using System.Linq;

namespace restAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
            var restaurant = _dbContext
              .Restaurants
              .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                return false;

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
            return true;
        }
        public bool Modify(ToModify obj, int id)
        {
            var restaurant = _dbContext
              .Restaurants
              .FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
                return false;

            restaurant.Name = obj.Name;
            restaurant.Description= obj.Description;
            restaurant.HasDelivery = obj.HasDelivery;

            _dbContext.SaveChanges();
            return true;
        }

        public RestaurantsDto GetById(int id)
        {
            var restaurant = _dbContext
               .Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                return null;

            var result = _mapper.Map<RestaurantsDto>(restaurant);
            return result;
        }
        public IEnumerable<RestaurantsDto> GetAll()
        {
            var restaurants = _dbContext
            .Restaurants;
            var restaurantsDtos = _mapper.Map<List<RestaurantsDto>>(restaurants);
            return restaurantsDtos;
        }
        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }

    }
}
