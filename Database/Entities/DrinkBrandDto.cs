using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class DrinkBrandDto
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}