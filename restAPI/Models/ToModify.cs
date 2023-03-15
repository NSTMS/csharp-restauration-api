using System.ComponentModel.DataAnnotations;
namespace restAPI.Models
{
    public class ToModify
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
    }
}
