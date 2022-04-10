namespace DrinksManagementSystem.Entities
{
    public class GeneralNotifiable : Notifiable
    {
        private bool _loading;

        public bool Loading { get => _loading; set => Set(ref _loading, value); }
    }
}

