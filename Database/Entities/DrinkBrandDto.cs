using SQLite;

namespace Database.Entities
{
    public class DrinkBrandDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}