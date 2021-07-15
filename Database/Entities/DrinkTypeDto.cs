using SQLite;

namespace Database.Entities
{
    public class DrinkTypeDto
    {
        [PrimaryKey, NotNull]
        public string Name { get; set; }
    }
}