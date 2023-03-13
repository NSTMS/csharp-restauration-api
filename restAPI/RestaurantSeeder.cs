using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restAPI.Entities;

namespace restAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant(){
                    Name = "KFC",
                    Category="Fast Food",
                    Description="Kentucky Fried Chickens",
                    ContactEmail="contact@kfc.com",
                    HasDelivery= true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="Nashiville Hot Chicken",
                            Price = 10.40M,
                        },
                        new Dish()
                        {
                            Name="Chicken Nuggets",
                            Price = 5.40M,
                        }
                    },
                    Address = new Address()
                    {
                        City="Kraków",
                        Street="Długa 5",
                        PostalCode="30-001"
                    }
                },
                new Restaurant(){
                    Name = "McDonalds",
                    Category="Fast Food",
                    Description="long random text",
                    ContactEmail="contact@mxdonalds.com",
                    HasDelivery= true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="McFlurry",
                            Price = 7.20M,
                        },
                        new Dish()
                        {
                            Name="BigMc",
                            Price = 15.90M,
                        }
                    },
                    Address = new Address()
                    {
                        City="Kraków",
                        Street="Floriańska 10",
                        PostalCode="30-521"
                    }
                }
            };
            return restaurants;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
