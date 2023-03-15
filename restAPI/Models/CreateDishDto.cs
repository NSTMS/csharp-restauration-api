using restAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace restAPI.Models
{
    public class CreateDishDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
