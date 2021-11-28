namespace DrinksManagementSystem.Entities
{
    public class EvaluationDrinkInfo : Notifiable
    {
        private double _fullPrice;
        private int _quantity;

        public double FullPrice { get => _fullPrice; set => Set(ref _fullPrice, value); }
        public int Quantity { get => _quantity; set => Set(ref _quantity, value); }
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
        public string DrinkName { get; set; }
    }
}