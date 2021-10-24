namespace Database.Models
{
    public interface IModel<TIdentifierType>
    {
        public TIdentifierType Id { get; set; }
    }
}