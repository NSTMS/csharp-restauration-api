using restAPI.Models;
using System.Collections.Generic;
using System.Threading;

namespace restAPI.Services
{
    public interface IRestaurantService
    {
        bool Delete(int id);
        bool Modify(ToModify obj, int id);
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantsDto> GetAll();
        RestaurantsDto GetById(int id);
    }
}