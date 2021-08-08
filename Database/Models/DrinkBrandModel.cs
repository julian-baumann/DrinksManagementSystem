using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class DrinkBrandModel
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}