using Npgsql;

namespace dormitory_system.Repositories.Interfaces;

public interface IRepository<T>
{
    public T Map(NpgsqlDataReader reader);
    public Task Add(T item);
    public Task Delete(T item);
}