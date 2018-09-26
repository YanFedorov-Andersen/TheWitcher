namespace TheWitcher.Domain.Mappers
{
    public interface IMapper<T, DTO>
    {
        DTO AutoMap(T item);
    }
}
