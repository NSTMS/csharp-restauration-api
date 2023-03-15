using restAPI.Models;
using System.Collections.Generic;

namespace restAPI.Services
{
    public interface IDishesService
    {
        bool AddToDishes(int id, CreateDishDto dto);
        IEnumerable<DishDto> GetAllDishes(int id);
        DishDto GetById(int id, int dishId);
    }
}