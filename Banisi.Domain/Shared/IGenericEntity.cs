namespace Banisi.Domain.Shared
{
    public interface IGenericEntity<T>
    {
        public T Id { get; set; }
    }
}