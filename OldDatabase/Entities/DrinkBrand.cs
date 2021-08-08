using SQLite;

namespace OldDatabase.Entities
{
    public class DrinkBrand
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}